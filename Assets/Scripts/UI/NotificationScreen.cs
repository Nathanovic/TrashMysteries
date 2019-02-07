using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationScreen : MonoBehaviour {

	public static NotificationScreen Instance { get; private set; }

	[Header("References")]
	[SerializeField] private Text notificationText;

	[Header("Settings")]
	[SerializeField] private float showDuration;

	private CanvasGroup canvasGroup;
	private Coroutine disableCoroutine;

	private void Awake() {
		Instance = this;
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0f;
	}

	public void Show(Vector3 position, string text) {
		transform.position = position;
		transform.eulerAngles = new Vector3(0f, Camera.main.transform.eulerAngles.y, 0f);
		notificationText.text = text;

		canvasGroup.alpha = 1f;
		disableCoroutine = StartCoroutine(DisableSelfOverTime());
	}

	private IEnumerator DisableSelfOverTime() {
		yield return new WaitForSeconds(showDuration);
		canvasGroup.alpha = 0f;
	}

}