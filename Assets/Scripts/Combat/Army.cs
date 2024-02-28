using AFSInterview.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Combat
{
    public class Army : MonoBehaviour
    {
        public static System.Action<Army> onNextAction;
        public string ArmyName => armyName;
        [SerializeField] string armyName;

        public int TurnNumber => turnNumber;
        private int turnNumber = 0;

        [SerializeField, Range(0f,16f)] float spawnRange;
        [SerializeField] List<Unit> initialArmy;
        public List<Unit> CurrentArmy => currentArmy;

        Army enemyArmy;

        List<Unit> currentArmy = new List<Unit>();
        Queue<Unit> actionQueue = new Queue<Unit>();

        public void SpawnUnits(Army enemyArmy)
        {
            this.enemyArmy = enemyArmy;
            foreach (var item in initialArmy)
            {
                var unit = Instantiate(item, RandomPosition(), Quaternion.identity);
                currentArmy.Add(unit);
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
            return currentArmy.Count > 0;
        }

        private Vector3 RandomPosition()
        {
            float xOffset = Random.Range(-spawnRange / 2, spawnRange / 2);
            float zOffset = Random.Range(-spawnRange / 2, spawnRange / 2);

            return new Vector3(transform.position.x + xOffset, 0, transform.position.z + zOffset);
        }

        public void ExecuteNextAction()
        {
            turnNumber++;
            Unit currentUnit = actionQueue.Dequeue();
            StartCoroutine(currentUnit.PerformAction(this, enemyArmy));
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