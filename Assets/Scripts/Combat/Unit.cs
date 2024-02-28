using UnityEngine;

namespace AFSInterview.Combat
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] UnitDataSO unitData;
        public UnitDataSO UnitData => unitData;

        int currentHealth;
        HealthBar healthBar;
        private void Awake()
        {
            healthBar = GetComponentInChildren<HealthBar>();
            currentHealth = unitData.MaxHealth;
        }

        public abstract void PerformAttack(Unit target);

        protected void DealDamage(Unit target)
        {
            int finalDamage = unitData.AttackDamage;

            if (target == null)
            {
                Debug.LogWarning("Selected target does not exist.");
                return;
            }

            foreach(UnitAttribute unitAttribute in target.UnitData.UnitAttribute) 
            {
                if(UnitData.AttackDamageOverride.UnitAttribute == unitAttribute)
                {
                    finalDamage = target.unitData.AttackDamage;
                    break;
                }
            }

            finalDamage = Mathf.Max(1, finalDamage - target.unitData.ArmorPoints);
            Debug.Log($"{gameObject.name} delt {finalDamage} damage ({unitData.AttackDamage} - {target.unitData.ArmorPoints}) to {target.gameObject.name}.");
            target.TakeDamage(finalDamage);
        }

        private void TakeDamage(int damage)
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);
            healthBar.UpdateHealth(currentHealth, unitData.MaxHealth);

            Debug.Log($"{gameObject.name} received {damage} damage, current health: {currentHealth}.");

            if (currentHealth <= 0)
                UnitDeath();
        }

        private void UnitDeath()
        {
            Debug.Log($"{gameObject.name} was eliminated from battlefield.");
            Destroy(gameObject);
        }
    }
}
