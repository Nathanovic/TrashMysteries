using System;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable {

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