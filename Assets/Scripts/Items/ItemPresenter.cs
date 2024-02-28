namespace AFSInterview.Items
{
	using UnityEngine;

	public class ItemPresenter : MonoBehaviour, IItemHolder
	{
		[SerializeField] private ItemSO item;
		private Renderer visuals;
        private void Awake()
        {
			visuals = gameObject.GetComponentInChildren<Renderer>();
            Init(item);
        }

        public void Init(ItemSO initialItem)
		{
			if(item == null) 
				item = initialItem;

			SetupVisuals();
        }

		void SetupVisuals()
		{
            visuals.material.color = item.ModelColor;
        }

		public ItemSO GetItem(bool disposeHolder)
		{
			if (disposeHolder)
				Destroy(gameObject);
			
			return item;
		}
	}
}