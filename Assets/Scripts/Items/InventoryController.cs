namespace AFSInterview.Items
{
    using System.Collections.Generic;
    using UnityEngine;

    public class InventoryController : MonoBehaviour
	{
		[SerializeField] private int money;
		[SerializeField] private List<ItemSO> items;

		public int Money => money;
		public int ItemsCount => items.Count;

        public void SellAllItemsUpToValue(int maxValue)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].Value > maxValue)
                    continue;
  
                money += items[i].Value;
                RemoveItem(i);
            }
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