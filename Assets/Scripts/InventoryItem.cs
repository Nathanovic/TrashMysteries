using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Custom/Item", order = 1)]
public class InventoryItem : ScriptableObject {

	public Sprite Visual { get { return visual; } }	
	[SerializeField] private Sprite visual;

}