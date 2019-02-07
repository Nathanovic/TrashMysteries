using UnityEngine;
using UnityEngine.UI;

public class VisualInventoryItem : MonoBehaviour {

	[Header("References")]
	[SerializeField] private Image itemImage;

	public void Show(InventoryItem item) {
		itemImage.sprite = item.Visual;
	}

}