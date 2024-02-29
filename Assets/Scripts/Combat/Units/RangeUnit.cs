using AFSInterview.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Combat
{
    public class RangeUnit : Unit
    {
        [SerializeField] protected Transform shootingPoint;
        [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected float bulletSpeed = 5f; 
        [SerializeField] protected float destroyDistance = 0.1f;
        public override IEnumerator PerformAction(Army ownArmy, Army enemyArmy)
        {
            var enemy = enemyArmy.CurrentArmy.SelectRandomElement();

            yield return new WaitForSeconds(0.5f);

            yield return StartCoroutine(RotateCharacter(enemy.transform.position));
            yield return StartCoroutine(ShootProjectile(bulletPrefab, shootingPoint, enemy.transform.position, destroyDistance, bulletSpeed));
            DealDamage(enemy);


            OnActionEnded?.Invoke();
        }
        

    }
}
