namespace AFSInterview.Items
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class InventoryManager : MonoBehaviour
    {
        public static Action<int> OnMoneyUpdated;
        [SerializeField] private int money;

        [SerializeField] private List<ItemSO> items = new List<ItemSO>();
        public int Money => money;
        public int ItemsCount => items.Count;

        private void Awake()
        {
            OnMoneyUpdated?.Invoke(money);
        }

        public void SellAllItemsUpToValue(int maxValue)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].SellValue > maxValue)
                    continue;

                money += items[i].SellValue;
                items.RemoveAt(i);
            }
            OnMoneyUpdated?.Invoke(money);
        }

        public void AddMoney(int amount)
        {
            money += amount;
            OnMoneyUpdated?.Invoke(money);
        }

        public void AddItem(ItemSO item)
		{
			items.Add(item);
        }

        public void RemoveItem(ItemSO itemToRemove)
        {
            items.Remove(itemToRemove);
        }
    }
}