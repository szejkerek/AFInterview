namespace AFSInterview.Items
{
	public interface IItemHolder
	{
		void Init(ItemSO item);

        ItemSO GetItem(bool disposeHolder);
	}
}