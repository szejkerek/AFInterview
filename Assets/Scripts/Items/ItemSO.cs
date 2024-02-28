namespace AFSInterview.Items
{
	using UnityEngine;

    [CreateAssetMenu(fileName = "Item", menuName = "Items/Default Item", order = 0)]
    public class ItemSO : ScriptableObject
	{
        [field: SerializeField] public string Name { private set; get; }
        [field: SerializeField] public int Value { private set; get; }
	}
}