namespace AFSInterview.Items
{
    using System.Collections.Generic;
    using UnityEngine;

    public class InventoryController : MonoBehaviour
	{
		[SerializeField] private int money;
		[SerializeField] private List<Item> items;

		public int Money => money;
		public int ItemsCount => items.Count;

        public void SellAllItemsUpToValue(int maxValue)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].Value > maxValue)
                    continue;
  
                money += items[i].Value;
                items.RemoveAt(i);
            }
        }

        public void AddItem(Item item)
		{
			items.Add(item);
		}
	}
}