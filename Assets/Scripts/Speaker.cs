using UnityEngine;

public class Speaker : MonoBehaviour {

	public Sprite Sprite { get { return sprite; } }
	public string Name { get { return name; } }
	public Conversation DefaultConversation { get { return defaultConversation; } }

	[SerializeField] private Sprite sprite;
	[SerializeField] private new string name;
	[SerializeField] private Conversation defaultConversation;

}