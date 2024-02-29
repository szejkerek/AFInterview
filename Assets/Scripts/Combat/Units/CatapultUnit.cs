using AFSInterview.Combat;
using AFSInterview.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatapultUnit : RangeUnit
{
    [SerializeField] float explosionRange = 3f;
    public override IEnumerator PerformAction(Army ownArmy, Army enemyArmy)
    {
        var enemy = enemyArmy.CurrentArmy.SelectRandomElement();

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(RotateCharacter(enemy.transform.position));
        yield return StartCoroutine(ShootProjectile(bulletPrefab, shootingPoint, enemy.transform.position, destroyDistance, bulletSpeed));

        foreach (Unit item in GetSurroundingUnits(enemy, enemyArmy, explosionRange))
        {
            DealDamage(item);
        }


        OnActionEnded?.Invoke();
    }

    Unit[] GetSurroundingUnits(Unit target, Army army, float range)
    {
        List<Unit> surroundingUnits = new List<Unit>();

        foreach (Unit unit in army.CurrentArmy)
        {
            float distance = Vector3.Distance(target.transform.position, unit.transform.position);
            if (distance <= range)
            {
                surroundingUnits.Add(unit);
            }     
        }

        return surroundingUnits.ToArray();
    }
}