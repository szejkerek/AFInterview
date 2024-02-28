using AFSInterview.Utility;
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

        List<Unit> currentArmy = new List<Unit>();
        Queue<Unit> actionQueue = new Queue<Unit>();
        public bool IsAlive()
        {
            currentArmy.RemoveAll(item => item.IsDead);
            actionQueue = new Queue<Unit>(actionQueue.Where(item => !item.IsDead));
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