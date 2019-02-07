using UnityEngine;
using UnityEngine.UI;

public class VisualInventoryItem : MonoBehaviour {

	private Image image;

	private void Awake() {
		image = GetComponent<Image>();
	}

	public void Show(InventoryItem item) {
		image.sprite = item.Visual;
	}

}