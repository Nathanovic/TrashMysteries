using System;
using UnityEngine;

public class Animal : MonoBehaviour, IInteractable {

	[SerializeField] protected ConversationScreen conversationScreen;
	[AssetDropdown("Conversations", typeof(Conversation))]
	[SerializeField] private Conversation defaultConversation;

	protected bool hasInteracted;

	public virtual bool CanInteract() {
		return !hasInteracted;
	}

	public string InteractionNotification() {
		return null;
	}

	public virtual void Interact(Action onFinishedInteraction) {
		hasInteracted = true;
		conversationScreen.Show(defaultConversation, onFinishedInteraction);
	}

}