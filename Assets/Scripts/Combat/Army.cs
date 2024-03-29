﻿using AFSInterview.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AFSInterview.Combat
{
    public class Army : MonoBehaviour
    {
        public static System.Action<Army> onNextAction;
        public int TurnNumber = 0;
        public string ArmyName => armyName;
        [SerializeField] string armyName;

        [SerializeField, Range(0f,16f)] float spawnRange;
        [SerializeField] List<Unit> initialArmy;
        public List<Unit> CurrentArmy => currentArmy;

        Army enemyArmy;
        List<Unit> currentArmy = new List<Unit>();
        Queue<Unit> actionQueue = new Queue<Unit>();

        public void SpawnUnits(Army enemyArmy)
        {
            this.enemyArmy = enemyArmy;

            foreach (var unitPrefab in initialArmy)
            {
                SpawnUnit(unitPrefab);
            }

            currentArmy.Shuffle();
            RefillTurnQueue();
        }

        public void RefillTurnQueue()
        {
            actionQueue.Clear();
            actionQueue = new Queue<Unit>(currentArmy);
        }


        public bool IsAlive()
        {
            currentArmy.RemoveAll(item => item.IsDead);
            actionQueue = new Queue<Unit>(actionQueue.Where(item => !item.IsDead));
            return currentArmy.Count > 0;
        }

        private void SpawnUnit(Unit unitPrefab)
        {
            var unit = Instantiate(unitPrefab, RandomPosition(), Quaternion.identity);
            currentArmy.Add(unit);
            unit.transform.parent = transform;
        }

        private Vector3 RandomPosition()
        {
            float xOffset = Random.Range(-spawnRange / 2, spawnRange / 2);
            float zOffset = Random.Range(-spawnRange / 2, spawnRange / 2);

            return new Vector3(transform.position.x + xOffset, 0, transform.position.z + zOffset);
        }

        public void ExecuteNextAction(Unit toPerform)
        {
            StartCoroutine(toPerform.PerformAction(this, enemyArmy));
        }

        public Unit GetUnitToPerform()
        {
            if(actionQueue.Count == 0)
                return null;

            Unit unit = actionQueue.Dequeue();

            if (TurnNumber % unit.UnitData.AttackInterval == 0)
            {
                return unit;
            }

            return null;
        }

        public bool ShouldChangeTurn()
        {
            return actionQueue.Count == 0;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange, 0.1f, spawnRange));
        }
    }
}