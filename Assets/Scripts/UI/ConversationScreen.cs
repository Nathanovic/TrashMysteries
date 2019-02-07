using System;
using UnityEngine;
using UnityEngine.UI;

public class ConversationScreen : MonoBehaviour {

	private CanvasGroup canvasGroup;

	[Header("References")]
	[SerializeField] private GameObject playerTextContainer;
	[SerializeField] private GameObject otherSpeakerTextContainer;
	[SerializeField] private Text playerNameText;
	[SerializeField] private Text otherNameText;
	[SerializeField] private Text conversationText;
	[SerializeField] private Character playerCharacter;
	[SerializeField] private Image playerImage;
	[SerializeField] private Image otherImage;

	[Header("Settings")]
	[SerializeField] private KeyCode continueKey;

	private Action onInteractionClosedCallback;
	private Conversation conversation;
	private int currentConversationTextIndex;

	private void Awake() {
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0f;
	}

	private void Update() {
		if(canvasGroup.alpha == 0f) { return; }
		if (Input.GetKeyUp(continueKey)) {
			currentConversationTextIndex++;
			ShowConversationText();
		}
	}

	public void Show(Conversation conversation, Action onDone) {
		this.conversation = conversation;
		onInteractionClosedCallback = onDone;

		playerNameText.text = playerCharacter.Name;
		otherNameText.text = conversation.Speaker.Name;

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

		playerTextContainer.SetActive(textInformation.IsPlayerText);
		otherSpeakerTextContainer.SetActive(!textInformation.IsPlayerText);

		playerImage.sprite = playerCharacter.GetVisuals(textInformation.PlayerEmote);
		otherImage.sprite = conversation.Speaker.GetVisuals(textInformation.SpeakerEmote);
	}
	
}