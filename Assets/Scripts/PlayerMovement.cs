using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[Header("Settings")]
	[SerializeField] private float moveSpeed;

	private new Rigidbody rigidbody;

	private void Awake() {
		rigidbody = GetComponent<Rigidbody>();
	}

	private void Update() {
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		Vector3 input = new Vector3(horizontalInput, 0f, verticalInput);
		if (horizontalInput != 0 && verticalInput != 0) {
			input = Vector3.ClampMagnitude(input, 1f);
		}

		Vector3 moveForce = input * moveSpeed;
		rigidbody.AddForce(moveForce);
	}

}