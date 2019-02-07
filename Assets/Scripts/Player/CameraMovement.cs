using UnityEngine;

public class CameraMovement : MonoBehaviour {

	[Header("Settings")]
	[SerializeField] private Transform playerTarget;
	[SerializeField] private float rotateSpeed;

	[Header("References")]
	[SerializeField] private Transform playerForward;

	private Vector3 previousPlayerPosition;
	private Vector3 playerOffset;

	private void Awake() {
		playerOffset = playerTarget.position - transform.position;
		previousPlayerPosition = playerTarget.position;
	}

	private void LateUpdate() {
		Vector3 playerMovement = playerTarget.position - previousPlayerPosition;
		transform.position = transform.position + playerMovement;
		previousPlayerPosition = playerTarget.position;

		Vector3 targetPosition = playerTarget.position + Vector3.up * playerOffset.y;
		float cameraInput = Input.GetAxis("HorizontalCameraMovement");
		transform.RotateAround(targetPosition, Vector3.up, rotateSpeed * -cameraInput * Time.deltaTime);
		transform.LookAt(playerTarget.position);

		playerForward.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
	}

}