using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Combat
{
    public class KnightUnit : Unit
    {
        public override IEnumerator PerformAction(Army ownArmy, Army enemyArmy)
        {
            yield return null;
        }
    }
}
