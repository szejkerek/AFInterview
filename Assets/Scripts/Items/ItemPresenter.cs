using UnityEngine;

namespace AFSInterview.Items
{
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
        public ItemSO GetItem(bool disposeHolder)
        {
            if (disposeHolder)
                Destroy(gameObject);

            return item;
        }

        void SetupVisuals()
		{
            visuals.material.color = item.ModelColor;
        }
	}
}