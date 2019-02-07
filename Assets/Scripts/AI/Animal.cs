using System;
using UnityEngine;

public class Animal : MonoBehaviour, IInteractable {

	[SerializeField]private ConversationScreen conversationScreen;
	[SerializeField] private Conversation defaultConversation;
	private bool hasInteractionText = true;

	public bool CanInteract() {
		return hasInteractionText;
	}

	public string InteractionNotification() {
		return null;
	}

	public void Interact(Action onFinishedInteraction) {
		hasInteractionText = false;
		conversationScreen.Show(defaultConversation, onFinishedInteraction);
	}

}