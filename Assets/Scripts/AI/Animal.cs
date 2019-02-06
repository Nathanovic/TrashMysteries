using System;
using UnityEngine;

[RequireComponent(typeof(Speaker))]
public class Animal : MonoBehaviour, IInteractable {

	[SerializeField]private ConversationScreen conversationScreen;
	private Speaker speaker;
	private bool hasInteractionText = true;

	private void Awake() {
		speaker = GetComponent<Speaker>();
	}

	public bool CanInteract() {
		return hasInteractionText;
	}

	public void Interact(Action onFinishedInteraction) {
		hasInteractionText = false;
		conversationScreen.Show(speaker, onFinishedInteraction);
	}

}