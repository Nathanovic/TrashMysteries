using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Custom/Character", order = 1)]
public class Character : ScriptableObject {

	public string Name { get { return name; } }

	[SerializeField] private new string name;
	[SerializeField] private Sprite neutralVisuals;
	[SerializeField] private Sprite happyVisuals;
	[SerializeField] private Sprite shockedVisuals;
	[SerializeField] private Sprite sadVisuals;

	public Sprite GetVisuals(CharacterEmote emote) {
		switch (emote) {
			case CharacterEmote.Neutral:
				return neutralVisuals;
			case CharacterEmote.Happy:
				return happyVisuals;
			case CharacterEmote.Shocked:
				return shockedVisuals;
			case CharacterEmote.Sad:
				return sadVisuals;
			default:
				Debug.LogWarning("Unknown character emote: " + emote.ToString());
				return neutralVisuals;
		}
	}

}