namespace AFSInterview.Items
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "MoneyBag", menuName = "Items/Money Bag")]
    public class MoneyBagItemSO : ItemSO, IUsableItem
    {
        public void Use(Inventory inventory)
        {
            inventory.AddMoney(SellValue);
        }
    }
}