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
        private bool isPlayable = true;

        private void Awake()
        {
            right.SpawnUnits(enemyArmy: left);
            left.SpawnUnits(enemyArmy: right);
            rightTurn = Random.Range(0, 2) == 0;

            Unit.OnActionEnded += OnEndTurn;
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
            if (!isPlayable)
                return;
            OnStartTurn();
            GetCurrentTurnArmy().ExecuteNextAction();
        }

        private void OnStartTurn()
        {
            isPlayable = false;
        }

        private void OnEndTurn()
        {
            Army currentArmy = GetCurrentTurnArmy();
            
            if (!GetOppositeTurnArmy().IsAlive())
            {
                Debug.Log("WIN");
                return;
            }

            if (currentArmy.ShouldChangeTurn())
            {
                currentArmy.RefillTurnQueue();
                rightTurn = !rightTurn;
            }

            Army.onNextAction?.Invoke(currentArmy);
            isPlayable = true;
        }
    }
}
