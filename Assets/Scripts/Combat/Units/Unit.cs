using System;
using System.Collections;
using UnityEngine;

namespace AFSInterview.Combat
{
    public abstract class Unit : MonoBehaviour
    {
        public static Action OnActionEnded;
        protected Vector3 startingPosition;

        [SerializeField] UnitDataSO unitData;
        public UnitDataSO UnitData => unitData;
        public bool IsDead => isDead;

        int currentHealth;
        HealthBar healthBar;
        bool isDead = false;

        private void Awake()
        {
            healthBar = GetComponentInChildren<HealthBar>();
            startingPosition = transform.position;
            currentHealth = unitData.MaxHealth;
        }

        public abstract IEnumerator PerformAction(Army ownArmy, Army enemyArmy);

        protected void RestoreHealth()
        {
            currentHealth = unitData.MaxHealth;
            healthBar.UpdateHealth(currentHealth, unitData.MaxHealth);

            Debug.Log($"{gameObject.name} health has been restored, current health: {currentHealth}.");
        }

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

        protected IEnumerator MoveCharacter(Vector3 position, float tooCloseRange = 0)
        {
            while (Vector3.Distance(transform.position, position) > tooCloseRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * UnitData.MovementSpeed);
                yield return null;
            }
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
            isDead = true;
            gameObject.SetActive(false);
            Debug.Log($"{gameObject.name} was eliminated from battlefield.");
        }
    }
}
