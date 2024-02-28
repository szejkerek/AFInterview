using AFSInterview.Items;
using TMPro;
using UnityEngine;

namespace AFSInterview
{
    public class UserInterfaceManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyDisplay;

        private void OnEnable() => InventoryManager.OnMoneyUpdated += UpdateMoneyDisplay;
        private void OnDisable() => InventoryManager.OnMoneyUpdated -= UpdateMoneyDisplay;

        private void UpdateMoneyDisplay(int newMoney)
        {
            moneyDisplay.text = $"Money: {newMoney}";
        }
    }
}