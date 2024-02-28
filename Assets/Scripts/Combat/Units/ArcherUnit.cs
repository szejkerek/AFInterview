using AFSInterview.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Combat
{
    public class ArcherUnit : Unit
    {
        [SerializeField] Transform shootingPoint;
        [SerializeField] GameObject arrowPrefab;
        [SerializeField] float arrowSpeed = 5f; 
        [SerializeField] float destroyDistance = 0.1f;
        public override IEnumerator PerformAction(Army ownArmy, Army enemyArmy)
        {
            var enemy = enemyArmy.CurrentArmy.SelectRandomElement();

            yield return new WaitForSeconds(0.5f);

            yield return StartCoroutine(RotateCharacter(enemy.transform.position));
            yield return StartCoroutine(ShootProjectile(arrowPrefab, shootingPoint, enemy.transform.position, destroyDistance, arrowSpeed));
            DealDamage(enemy);


            OnActionEnded?.Invoke();
        }
        

    }
}
