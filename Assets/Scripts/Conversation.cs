using System;
using UnityEngine;

[CreateAssetMenu(fileName="Conversation", menuName="Custom/Conversation", order=1)]
public class Conversation : ScriptableObject {

	[Serializable]
	public class Lines {
		[SerializeField] public string Text;
		[SerializeField] public CharacterEmote SpeakerEmote;
		[SerializeField] public CharacterEmote PlayerEmote;
		[SerializeField] public bool IsPlayerText;
	}

	[AssetDropdown("Characters", typeof(Character))]
	[SerializeField] public Character Speaker;
	[SerializeField] public Lines[] AllLines;

}