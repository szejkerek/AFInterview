using System;
using UnityEngine;

namespace AFSInterview.Combat
{
    [Serializable]
    public struct DamageOverride
    {
        [field: SerializeField] public UnitAttribute UnitAttribute { private set; get; }
        [field: SerializeField] public int AttackDamage { private set; get; }
    }
}
