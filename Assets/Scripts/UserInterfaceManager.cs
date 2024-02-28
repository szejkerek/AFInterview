using AFSInterview.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AFSInterview
{
    public class UserInterfaceManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyDisplay;

        private void OnEnable() => Inventory.onMoneyUpdated += RefreshDisplayText;
        private void OnDisable() => Inventory.onMoneyUpdated -= RefreshDisplayText;

        private void RefreshDisplayText(int newValue)
        {
            moneyDisplay.text = $"Money: {newValue}";
        }
    }
}
