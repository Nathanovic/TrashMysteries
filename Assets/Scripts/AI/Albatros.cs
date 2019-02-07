using System;
using UnityEngine;

public class Albatros : Animal {

	[Header("Settings")]
	[AssetDropdown("Conversations", typeof(Conversation))]
	[SerializeField] private Conversation wrongItemConversation;
	[AssetDropdown("Conversations", typeof(Conversation))]
	[SerializeField] private Conversation correctItemConversation;
	[AssetDropdown("Items", typeof(InventoryItem))]
	[SerializeField] private InventoryItem requiredItem;

	public override bool CanInteract() {
		if (hasInteracted) { return Inventory.Instance.ItemCount() > 0; }
		return base.CanInteract();
	}

	public override void Interact(Action onFinishedInteraction) {
		hasInteracted = true;
		if (Inventory.Instance.ItemCount() == 0) {
			base.Interact(onFinishedInteraction);
			return;
		}

		bool playerHasCorrectItem = Inventory.Instance.ContainsItem(requiredItem);
		if (playerHasCorrectItem) {
			conversationScreen.Show(correctItemConversation, () => {
				GameManager.Instance.CompleteGame();
				onFinishedInteraction?.Invoke();
			});
		} else {
			conversationScreen.Show(wrongItemConversation, () => {
				onFinishedInteraction?.Invoke();
			});
		}
	}

}
