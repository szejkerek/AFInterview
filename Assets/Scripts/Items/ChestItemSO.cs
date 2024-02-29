using AFSInterview.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Items
{
    [CreateAssetMenu(fileName = "ChestItem", menuName = "Items/Chest Item")]
    public class ChestItemSO : ItemSO, IUsableItem
    {
        [field: SerializeField] public List<ItemSO> PossibleDrop { private set; get; }
        public void Use(InventoryManager inventory)
        {
            if (PossibleDrop.Count == 0)
                return;

            inventory.AddItem(PossibleDrop.SelectRandomElement());
        }
    }
}