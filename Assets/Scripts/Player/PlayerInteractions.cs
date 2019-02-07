using UnityEngine;

public class PlayerInteractions : MonoBehaviour {
	
	public bool IsInteracting { get; private set; }

	private IInteractable currentInteractable;

	private void Update() {
		if(currentInteractable == null) { return; }
		if (Input.GetButtonDown("PickUp")) {
			currentInteractable.Interact(null);
			currentInteractable = null;
		}
	}

	private void OnTriggerEnter(Collider other) {
		IInteractable interactable = other.GetComponent<IInteractable>();

		if (interactable == null) { return; }
		if (IsInteracting) { return; }
		if (!interactable.CanInteract()) { return; }

		string notificationText = interactable.InteractionNotification();
		if (notificationText == null) {
			currentInteractable = null;
			IsInteracting = true;
			interactable.Interact(() => {
				IsInteracting = false;
			});
		} else {
			NotificationScreen.Instance.Show(other.transform.position, notificationText);
			currentInteractable = interactable;
		}
	}

	private void OnTriggerExit(Collider other) {
		IInteractable interactable = other.GetComponent<IInteractable>();
		if (interactable == null) { return; }
		if (interactable == currentInteractable) {
			interactable = null;
		}
	}

}