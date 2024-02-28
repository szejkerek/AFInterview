namespace AFSInterview.Items
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "MoneyBag", menuName = "Items/Money Bag")]
    public class MoneyBagItemSO : ItemSO, IUsableItem
    {
        public void Use(InventoryManager inventory)
        {
            inventory.AddMoney(SellValue);
        }
    }
}