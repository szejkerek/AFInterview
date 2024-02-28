namespace AFSInterview.Items
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Inventory
    {
        public static Action<int> onMoneyUpdated;

        [SerializeField] private int money;
        [SerializeField] private List<ItemSO> items = new List<ItemSO>();

        public int Money => money;
        public int ItemsCount => items.Count;

        public Inventory()
        {
            if (money > 0)
                onMoneyUpdated?.Invoke(money);
        }

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
            onMoneyUpdated?.Invoke(money);
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
    }
}