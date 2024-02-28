namespace AFSInterview.Items
{
	using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

	public class InventoryController : MonoBehaviour
	{
		[SerializeField] private List<Item> items;
		[SerializeField] private int money;

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