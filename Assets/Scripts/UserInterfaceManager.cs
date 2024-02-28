using AFSInterview.Combat;
using AFSInterview.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AFSInterview
{
    public class UserInterfaceManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyDisplay;
        [SerializeField] private TextMeshProUGUI turnText;
        [SerializeField] private Button nextActionButton;

        private void OnEnable()
        {
            InventoryManager.OnMoneyUpdated += UpdateMoneyDisplay;
            Army.onNextAction += UpdateTurnDisplay;
        }

        private void OnDisable()
        {
            InventoryManager.OnMoneyUpdated -= UpdateMoneyDisplay;
            Army.onNextAction -= UpdateTurnDisplay;
        }

        private void UpdateMoneyDisplay(int newMoney)
        {
            moneyDisplay.text = $"Money: {newMoney}";
        }
        
        private void UpdateTurnDisplay(Army army)
        {
            turnText.text = $"Turn number {army.TurnNumber} of {army.ArmyName}.";
        }
    }
}