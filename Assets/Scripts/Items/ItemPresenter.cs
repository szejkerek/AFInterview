namespace AFSInterview.Items
{
	using UnityEngine;

	public class ItemPresenter : MonoBehaviour, IItemHolder
	{
		[SerializeField] private ItemSO item;
        
		public void Init(ItemSO item)
		{
			if(item != null) 
				this.item = item;

			SetupVisuals();

        }

		void SetupVisuals()
		{
			
		}

		public ItemSO GetItem(bool disposeHolder)
		{
			if (disposeHolder)
				Destroy(gameObject);
			
			return item;
		}
	}
}