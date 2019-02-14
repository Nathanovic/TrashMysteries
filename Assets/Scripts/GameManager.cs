using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager instance;
	public static GameManager Instance {
		get {
			if (instance == null) {
				instance = new GameObject("Game Manager").AddComponent<GameManager>();
			}

			return instance;
		}
	}

	public bool IsGamePlaying { get; private set; }

	private void Awake() {
		IsGamePlaying = true;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Debug.Log("Reload scene for testing purposes");
			SceneManager.LoadScene(0);
		}
	}

	public void CompleteGame() {
		IsGamePlaying = false;
		RealLifeChallengeScreen.Instance.Show();
	}

}