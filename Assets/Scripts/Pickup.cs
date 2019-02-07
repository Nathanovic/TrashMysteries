using System;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable {

	private const string PICK_UP_TEXT = "Press space to pick up ";

	[AssetDropdown("Items", typeof(InventoryItem))]
	[SerializeField] public InventoryItem Item;

	public bool CanInteract() {
		return true;
	}

	public string InteractionNotification() {
		return PICK_UP_TEXT + Item.name;
	}

	public void Interact(Action onFinishedInteraction) {
		Inventory.Instance.AddItem(Item);
		onFinishedInteraction?.Invoke();
		Destroy(gameObject);
	}

}