namespace AFSInterview.Items
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Item", menuName = "Items/Default Item")]
    public class ItemSO : ScriptableObject
	{
        [field: SerializeField] public string Name { private set; get; }
        [field: SerializeField] public int SellValue { private set; get; }
	}
}