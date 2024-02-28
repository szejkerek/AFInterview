using AFSInterview.Combat;
using System.Collections;
using System.Linq;
using UnityEngine;

public class DruidUnit : Unit
{
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject healingPrefab;
    [SerializeField] float arrowSpeed = 5f;
    [SerializeField] float destroyDistance = 0.1f;
    public override IEnumerator PerformAction(Army ownArmy, Army enemyArmy)
    {
        var ally = FindLowestHealthUnit(ownArmy);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(RotateCharacter(ally.transform.position));
        yield return StartCoroutine(ShootProjectile(healingPrefab, shootingPoint, ally.transform.position, destroyDistance, arrowSpeed));
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