using AFSInterview.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AFSInterview.Combat
{
    public class Army : MonoBehaviour
    {
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
            return currentArmy.Any(item => !item.IsDead);
        }

        private Vector3 RandomPosition()
        {
            float xOffset = Random.Range(-spawnRange / 2, spawnRange / 2);
            float zOffset = Random.Range(-spawnRange / 2, spawnRange / 2);

            return new Vector3(transform.position.x + xOffset, 0, transform.position.z + zOffset);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange, 0.1f, spawnRange));
        }

        public void ExecuteNextAction()
        {
            Unit currentUnit = actionQueue.Dequeue();
            StartCoroutine(currentUnit.PerformAction(this, enemyArmy));
            actionQueue.Enqueue(currentUnit);
        }

        public bool ShouldChangeTurn()
        {
            return actionQueue.Count == 0;
        }
    }
}