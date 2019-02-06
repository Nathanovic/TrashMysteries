using System;

public interface IInteractable {
	bool CanInteract();
	void Interact(Action onFinishedInteraction);
}