using UnityEngine;

public class RealLifeChallengeScreen : MonoBehaviour {

	public static RealLifeChallengeScreen Instance { get; private set; }

	private CanvasGroup canvasGroup;

	private void Awake() {
		Instance = this;
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0f;
	}

	public void Show() {
		canvasGroup.alpha = 1f;
	}

}