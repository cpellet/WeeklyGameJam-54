using UnityEngine.UI;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour {

	public Slider healthSlider;
	public Slider memoryRecoveredSlider;
	public Item[] playerItems;
	public GameObject content;
	private int activeItems;
	private GameObject slot;

	void Start(){
		healthSlider.value = 100;
		memoryRecoveredSlider.value = 0;
		InitialiseItems();
		activeItems = 0;
		AddItem();
	}

	void InitialiseItems(){
		for (int i = 0; i < (content.transform.childCount); i++){
			content.transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	void AddItem(){
		slot = content.transform.GetChild(activeItems+1).gameObject;
		slot.SetActive(true);
	}

}
