using System;

public interface IInteractable {
	bool CanInteract();
	string InteractionNotification();
	void Interact(Action onFinishedInteraction);
}