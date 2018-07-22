using UnityEngine;
using UnityEngine.UI;

public class Item {

	public string name;
	public enum type{weapon, food, souvenir, collectible};
	public type itemType;
	public Image preview;
}
