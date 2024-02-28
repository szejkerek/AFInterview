using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AFSInterview.Combat
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image healthbar;

        Camera mainCamera;
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            float ratio = currentHealth / maxHealth;
            ratio = Mathf.Clamp01(ratio);

            healthbar.fillAmount = ratio;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        }
    }
}
