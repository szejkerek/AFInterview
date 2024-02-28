namespace AFSInterview.Items
{
	using UnityEngine;

	public class ItemPresenter : MonoBehaviour, IItemHolder
	{
		private ItemSO item;
		private Renderer visuals;
        private void Awake()
        {
			visuals = gameObject.GetComponentInChildren<Renderer>();
        }

        public void Init(ItemSO initialItem)
		{
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