using System;
using UnityEngine;
using UnityEngine.UI;

public class ConversationScreen : MonoBehaviour {

	private CanvasGroup canvasGroup;

	[Header("References")]
	[SerializeField] private Text titleText;
	[SerializeField] private Text conversationText;
	[SerializeField] private Character playerCharacter;
	[SerializeField] private Image playerSpeakerImage;
	[SerializeField] private Image otherSpeakerImage;

	[Header("Settings")]
	[SerializeField] private KeyCode continueKey;

	private Action onInteractionClosedCallback;
	private bool isInteracting;
	private Conversation conversation;
	private int currentConversationTextIndex;

	private void Awake() {
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0f;
	}

	private void Update() {
		if (Input.GetKeyUp(continueKey)) {
			currentConversationTextIndex++;
			ShowConversationText();
		}
	}

	public void Show(Conversation conversation, Action onDone) {
		this.conversation = conversation;
		onInteractionClosedCallback = onDone;

		canvasGroup.alpha = 1f;
		currentConversationTextIndex = 0;
		ShowConversationText();
	}

	private void ShowConversationText() {
		if (currentConversationTextIndex >= conversation.AllLines.Length) {
			canvasGroup.alpha = 0f;
			onInteractionClosedCallback?.Invoke();
			return;
		}

		Conversation.Lines textInformation = conversation.AllLines[currentConversationTextIndex];
		conversationText.text = textInformation.Text;
		string speakerName = textInformation.IsPlayerText ? playerCharacter.Name : conversation.Speaker.Name;
		titleText.text = speakerName + ":";
		playerSpeakerImage.sprite = playerCharacter.GetVisuals(textInformation.PlayerEmote);
		otherSpeakerImage.sprite = playerCharacter.GetVisuals(textInformation.SpeakerEmote);
	}
	
}