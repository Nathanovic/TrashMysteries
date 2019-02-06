using System;
using UnityEngine;
using UnityEngine.UI;

public class ConversationScreen : MonoBehaviour {

	private CanvasGroup canvasGroup;

	[Header("References")]
	[SerializeField] private Text titleText;
	[SerializeField] private Text conversationText;
	[SerializeField] private Speaker playerSpeaker;
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

	public void Show(Speaker speaker, Action onDone) {
		onInteractionClosedCallback = onDone;
		canvasGroup.alpha = 1f;
		currentConversationTextIndex = 0;
		titleText.text = speaker.Name + ":";
		playerSpeakerImage.sprite = playerSpeaker.Sprite;
		otherSpeakerImage.sprite = speaker.Sprite;
		conversation = speaker.DefaultConversation;
	}

	private void ShowConversationText() {
		if (currentConversationTextIndex >= conversation.Texts.Length) {
			canvasGroup.alpha = 0f;
			onInteractionClosedCallback?.Invoke();
			return;
		}

		conversationText.text = conversation.Texts[currentConversationTextIndex];
	}
	
}