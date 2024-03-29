using AFSInterview.Utility;
using System.Collections;
using UnityEngine;

namespace AFSInterview.Combat
{
    public class MeeleUnit : Unit
    {
        public override IEnumerator PerformAction(Army ownArmy, Army enemyArmy)
        {
            var enemy = enemyArmy.CurrentArmy.SelectRandomElement();

            yield return StartCoroutine(RotateCharacter(enemy.transform.position));
            yield return StartCoroutine(MoveCharacter(enemy.transform.position, 2f));
            yield return new WaitForSeconds(0.5f);

            DealDamage(enemy);

            yield return StartCoroutine(MoveCharacter(startingPosition));

            OnActionEnded?.Invoke();
        }
    }
}
