using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AFSInterview.Combat
{
    public class FloatingInformation : MonoBehaviour
    {
        [SerializeField] TMP_Text unitName;
        [SerializeField] TMP_Text healthText;
        [SerializeField] Image healthbar;

        Camera mainCamera;
        private void Awake()
        {
            mainCamera = Camera.main;
        }
        public void SetUnitName(string name)
        {
            if (name.EndsWith("(Clone)"))
            {
                name = name.Substring(0, name.Length - "(Clone)".Length);
            }
            unitName.text = name;
        }
        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            float ratio = currentHealth / maxHealth;
            ratio = Mathf.Clamp01(ratio);

            healthText.text = $"{currentHealth}/{maxHealth}";
            healthbar.fillAmount = ratio;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        }
    }
}
