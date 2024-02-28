using System;
using System.Collections;
using UnityEngine;

namespace AFSInterview.Combat
{
    public abstract class Unit : MonoBehaviour
    {
        public static Action OnActionEnded;
        protected Vector3 startingPosition;

        [SerializeField] float movementSpeed = 35f;
        [SerializeField] UnitDataSO unitData;
        public int CurrentHealth => currentHealth;
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

        public void RestoreHealth()
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

        protected IEnumerator MoveCharacter(Vector3 position, float tooCloseRange = 0.3f)
        {
            float startTime = Time.time;
            while (Vector3.Distance(transform.position, position) > tooCloseRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * movementSpeed);
                if (Time.time - startTime >= 1.5f)
                {
                    break;
                }
                yield return null;
            }
        }

        protected IEnumerator RotateCharacter(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float startTime = Time.time;
            while (transform.rotation != targetRotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
                if (Time.time - startTime >= 1.5f)
                {
                    break;
                }
                yield return null;
            }
        }

        protected IEnumerator ShootProjectile(GameObject prefab, Transform shootingPoint, Vector3 targetPosition, float destroyDistance, float speed)
        {
            GameObject projectile = Instantiate(prefab, shootingPoint.position, Quaternion.identity);
            Vector3 direction = (targetPosition - projectile.transform.position).normalized;
            projectile.transform.rotation = Quaternion.LookRotation(direction);

            float startTime = Time.time;

            while (Vector3.Distance(projectile.transform.position, targetPosition) > destroyDistance)
            {
                projectile.transform.position += direction * speed * Time.deltaTime;
                if (Time.time - startTime >= 1.5f)
                {
                    break; 
                }

                yield return null;
            }

            Destroy(projectile);
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
