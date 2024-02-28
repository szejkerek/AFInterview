using AFSInterview.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Combat
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] Army right;
        [SerializeField] Army left;
        bool rightTurn;

        private void Awake()
        {
            right.SpawnUnits(enemyArmy: left);
            left.SpawnUnits(enemyArmy: right);
            rightTurn = Random.Range(0, 2) == 0;
        }

        public Army GetCurrentTurnArmy()
        {
            return rightTurn ? right : left;
        }

        public Army GetOppositeTurnArmy()
        {
            return rightTurn ? left : right;
        }

        public void ExecuteNextTurn()
        {
            Army currentArmy = GetCurrentTurnArmy();
            currentArmy.ExecuteNextAction();

            if (currentArmy.ShouldChangeTurn())
            {
                currentArmy.RefillTurnQueue();
                rightTurn = !rightTurn;
            }
            
        }
    }
}
