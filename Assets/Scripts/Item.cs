using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item {

	public string name;
	public enum type{weapon, food, souvenir, collectible};
	public type itemType;
	public Sprite preview;
	public GameObject prefab;
}
