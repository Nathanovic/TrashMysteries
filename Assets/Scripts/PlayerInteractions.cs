using UnityEngine;

public class PlayerInteractions : MonoBehaviour {
	
	public bool IsInteracting { get; private set; }

	private void OnTriggerEnter(Collider other) {
		IInteractable interactable = other.GetComponent<IInteractable>();

		if (interactable == null) { return; }
		if (IsInteracting) { return; }
		if (!interactable.CanInteract()) { return; }

		IsInteracting = true;
		interactable.Interact(() => {
			IsInteracting = false;
		});
	}

}