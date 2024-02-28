using System;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Combat/Unit Data")]
    public class UnitDataSO : ScriptableObject
    {
        [field: SerializeField] public UnitAttribute[] UnitAttribute { private set; get; }
        [field: SerializeField] public int MaxHealth { private set; get; }
        [field: SerializeField] public int ArmorPoints { private set; get; }
        [field: SerializeField] public int AttackInterval { private set; get; }
        [field: SerializeField] public int AttackDamage { private set; get; }
        [field: SerializeField] public DamageOverride AttackDamageOverride { private set; get; }
    }

    [Serializable]
    public enum UnitAttribute
    {
        Light,
        Armored,
        Mechanical
    }

    [Serializable]
    public struct DamageOverride
    {
        [field: SerializeField] public UnitAttribute UnitAttribute { private set; get; }
        [field: SerializeField] public int AttackDamage { private set; get; }
    }
}
