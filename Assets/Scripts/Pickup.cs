using System;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable {

	[AssetDropdown("Items", typeof(InventoryItem))]
	[SerializeField] public InventoryItem Item;

	public bool CanInteract() {
		return true;
	}

	public void Interact(Action onFinishedInteraction) {
		Inventory.Instance.AddItem(Item);
		onFinishedInteraction?.Invoke();
		Destroy(gameObject);
	}

}