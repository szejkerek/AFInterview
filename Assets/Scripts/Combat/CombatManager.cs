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

        private void Start()
        {
            right.SpawnUnits(enemyArmy: left);
            left.SpawnUnits(enemyArmy: right);
            rightTurn = Random.Range(0, 2) == 0;
            Army.onNextAction?.Invoke(GetCurrentTurnArmy());
            Unit.OnActionEnded += () => OnEndTurn();
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

            Army current = GetCurrentTurnArmy();
            Unit performUnit = current.GetUnitToPerform();

            if (performUnit != null)
            {
                OnStartTurn();
                current.ExecuteNextAction(performUnit);
            }
            else
            {
                Debug.Log("No units left to perform actions.");
                OnEndTurn(true);
            }
        }

        private void OnStartTurn()
        {
            isPlayable = false;
            Army.onNextAction?.Invoke(GetCurrentTurnArmy());
        }

        private void OnEndTurn(bool reroll = false)
        {
            Army currentArmy = GetCurrentTurnArmy();
            
            if (!GetOppositeTurnArmy().IsAlive())
            {
                Debug.Log("WIN");
                return;
            }

            if (currentArmy.ShouldChangeTurn())
            {
                ChangeTurn(currentArmy);
            }

            if (reroll)
            {
                ExecuteNextTurn();
            }

            isPlayable = true;
        }

        private void ChangeTurn(Army currentArmy)
        {
            currentArmy.TurnNumber++;
            currentArmy.RefillTurnQueue();
            rightTurn = !rightTurn;
        }
    }
}
