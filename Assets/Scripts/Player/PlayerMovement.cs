using UnityEngine;

[RequireComponent(typeof(PlayerInteractions), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

	[Header("Settings")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private Transform cameraForward;

	private new Rigidbody rigidbody;
	private PlayerInteractions interactionScript;

	private void Awake() {
		rigidbody = GetComponent<Rigidbody>();
		interactionScript = GetComponent<PlayerInteractions>();
	}

	private void Update() {
		if (interactionScript.IsInteracting) { return; }

		float horizontalInput = Input.GetAxis("HorizontalPlayerMovement");
		float verticalInput = Input.GetAxis("VerticalPlayerMovement");
		Vector3 input = new Vector3(horizontalInput, 0f, verticalInput);
		if (horizontalInput != 0f && verticalInput != 0f) {
			input = Vector3.ClampMagnitude(input, 1f);
		}

		if (input != Vector3.zero) {
			Vector3 relativeInput = cameraForward.TransformDirection(input);
			transform.forward = relativeInput;
			rigidbody.velocity = relativeInput * moveSpeed;
		}
	}

}