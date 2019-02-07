using UnityEngine;

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

	public void CompleteGame() {
		IsGamePlaying = false;
		RealLifeChallengeScreen.Instance.Show();
	}

}