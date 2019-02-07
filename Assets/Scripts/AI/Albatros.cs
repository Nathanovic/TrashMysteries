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
		if (!hasInteracted) {
			base.Interact(onFinishedInteraction);
			return;
		}

		hasInteracted = true;
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
