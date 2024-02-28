namespace AFSInterview.Items
{
    using TMPro;
    using UnityEngine;

    public class ItemsManager : MonoBehaviour
	{
        [SerializeField] private InventoryController inventoryController;

        [Header("Items proporties")]
        [SerializeField] private int itemSellMaxValue;
        [SerializeField] private Transform itemSpawnParent;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private BoxCollider itemSpawnArea;
        [SerializeField] private float itemSpawnInterval;

        [Header("User interface")]
        [SerializeField] private TextMeshProUGUI moneyDisplay;

        private float nextItemSpawnTime;
		private Camera mainCamera;
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
		{
			TrySpawnNewItem();
			
			if (Input.GetMouseButtonDown(0))
				TryPickUpItem();
			
			if (Input.GetKeyDown(KeyCode.Space))
				inventoryController.SellAllItemsUpToValue(itemSellMaxValue);

            moneyDisplay.text = $"Money: {inventoryController.Money}";
        }

		private void TrySpawnNewItem()
		{
            if (Time.time < nextItemSpawnTime)
                return;

            nextItemSpawnTime = Time.time + itemSpawnInterval;

            Bounds spawnAreaBounds = itemSpawnArea.bounds;

            float randomX = Random.Range(spawnAreaBounds.min.x, spawnAreaBounds.max.x);
            float randomZ = Random.Range(spawnAreaBounds.min.z, spawnAreaBounds.max.z);
            Vector3 position = new Vector3(randomX, 0f, randomZ);

            Instantiate(itemPrefab, position, Quaternion.identity, itemSpawnParent);
		}

		private void TryPickUpItem()
		{
			var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			var layerMask = LayerMask.GetMask("Item");
			if (!Physics.Raycast(ray, out var hit, 100f, layerMask) || !hit.collider.TryGetComponent<IItemHolder>(out var itemHolder))
				return;
			
			var item = itemHolder.GetItem(disposeHolder: true);
            inventoryController.AddItem(item);
            Debug.Log($"Picked up {item.Name} with value of {item.Value} and now have {inventoryController.ItemsCount} items.");
        }
	}
}