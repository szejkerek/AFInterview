using AFSInterview.Combat;
using System.Collections;
using System.Linq;
using UnityEngine;

public class HealUnit : RangeUnit
{
    public override IEnumerator PerformAction(Army ownArmy, Army enemyArmy)
    {
        var ally = FindLowestHealthUnit(ownArmy);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(RotateCharacter(ally.transform.position));
        yield return StartCoroutine(ShootProjectile(bulletPrefab, shootingPoint, ally.transform.position, destroyDistance, bulletSpeed));
        ally.RestoreHealth();


        OnActionEnded?.Invoke();
    }

    Unit FindLowestHealthUnit(Army army)
    {
        return army.CurrentArmy
                   .OrderBy(unit => unit.CurrentHealth)
                   .FirstOrDefault();
    }
}