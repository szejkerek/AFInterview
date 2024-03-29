﻿namespace AFSInterview.Items
{
    using AFSInterview.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class ItemsManager : MonoBehaviour
	{
        [SerializeField] private InventoryManager inventory;
        [SerializeField] private List<ItemSO> possibleItems;

        [Header("Items proporties")]
        [SerializeField] private ItemPresenter itemPresenterPrefab;
        [SerializeField] private LayerMask itemLayerMask;
        [SerializeField] private Transform itemSpawnParent;
        [SerializeField] private BoxCollider itemSpawnArea;
        [SerializeField] private int itemSellMaxValue;
        [SerializeField] private float itemSpawnInterval;

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
				inventory.SellAllItemsUpToValue(itemSellMaxValue);     
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

            var itemPresenter = Instantiate(itemPresenterPrefab, position, Quaternion.identity, itemSpawnParent);
            itemPresenter.Init(possibleItems.SelectRandomElement());
		}

		private void TryPickUpItem()
		{
			var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			if (!Physics.Raycast(ray, out var hit, 100f, itemLayerMask) || !hit.collider.TryGetComponent<IItemHolder>(out var itemHolder))
				return;		
			ItemSO item = itemHolder.GetItem(disposeHolder: true);

            IUsableItem usableItem = item as IUsableItem;
            if (usableItem != null)
            {
                usableItem.Use(inventory);
            }
            else
            {
                inventory.AddItem(item);
            }

            Debug.Log($"Picked up {item.Name} with value of {item.SellValue} and now have {inventory.ItemsCount} items.");
        }
	}
}