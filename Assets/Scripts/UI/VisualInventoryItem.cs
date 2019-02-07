using UnityEngine;
using UnityEngine.UI;

public class VisualInventoryItem : MonoBehaviour {

	public InventoryItem InventoryItem { get; private set; }

	[Header("References")]
	[SerializeField] private Image itemImage;

	public void Show(InventoryItem item) {
		InventoryItem = item;
		itemImage.sprite = item.Visual;
	}

}