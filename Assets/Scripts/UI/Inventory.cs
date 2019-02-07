using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public static Inventory Instance { get; private set; }

	[Header("References")]
	[SerializeField] private VisualInventoryItem itemTemplate;

	private List<VisualInventoryItem> activeItems;
	private List<VisualInventoryItem> deactivatedItems;

	private void Awake() {
		Instance = this;
		activeItems = new List<VisualInventoryItem>();
		deactivatedItems = new List<VisualInventoryItem>();
	}

	public void AddItem(InventoryItem item) {
		VisualInventoryItem newVisualItem = null;
		if (deactivatedItems.Count > 0) {
			newVisualItem = deactivatedItems[0];
			deactivatedItems.RemoveAt(0);
		} else {
			newVisualItem = Instantiate(itemTemplate, transform);
			newVisualItem.gameObject.name = "ItemVisual";
		}
		
		newVisualItem.gameObject.SetActive(true);
		newVisualItem.Show(item);
		activeItems.Add(newVisualItem);
	}

	public bool ContainsItem(InventoryItem item) {
		if(item == null) {
			Debug.Log("Contains-item-check with item parameter value 'null'");
			return false;
		}

		foreach (VisualInventoryItem inventoryItem in activeItems) {
			if (inventoryItem.InventoryItem == item) {
				return true;
			}
		}
		return false;
	}

	public int ItemCount() {
		return activeItems.Count;
	}

}