namespace AFSInterview.Items
{
    using System;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    [Serializable]
    public class InventoryManager : MonoBehaviour
    {

        [SerializeField] private int money;
        [SerializeField] private TextMeshProUGUI moneyDisplay;
        [SerializeField] private List<ItemSO> items = new List<ItemSO>();
        public int Money => money;
        public int ItemsCount => items.Count;

        public void SellAllItemsUpToValue(int maxValue)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].SellValue > maxValue)
                    continue;

                money += items[i].SellValue;
                RemoveItem(i);
            }
        }

        public void AddMoney(int amount)
        {
            money += amount;
            UpdateMoneyDisplay();
        }

        public void AddItem(ItemSO item)
		{
			items.Add(item);
        }

        void RemoveItem(int indexToRemove)
        {
            if (indexToRemove < 0 || indexToRemove >= items.Count)
                return;

            items.RemoveAt(indexToRemove);
        }

        public void RemoveItem(ItemSO itemToRemove)
        {
            items.Remove(itemToRemove);
        }
        private void UpdateMoneyDisplay()
        {
            moneyDisplay.text = $"Money: {money}";
        }
    }
}