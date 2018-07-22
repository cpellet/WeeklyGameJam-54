using UnityEngine.UI;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour {

	public Slider healthSlider;
	public Slider memoryRecoveredSlider;
	public Item[] playerItems;
	public GameObject content;
	private int activeItems;
	private GameObject slot;
	private Image slotImage;
	private int selectedSlot;
	private Button[] slots;

	void Start(){
		healthSlider.value = 100;
		memoryRecoveredSlider.value = 0;
		InitialiseItems();
		activeItems = 0;
		selectedSlot = 0;
	}

	void Update(){
		if (activeItems != 0){
			if (Input.GetAxis("Mouse ScrollWheel") > 0f){
				if(selectedSlot == activeItems){
					selectedSlot = 1;
				}else{
					selectedSlot ++;
				}
				Debug.Log(selectedSlot);
			}else if (Input.GetAxis("Mouse ScrollWheel") < 0f){
				if (selectedSlot == 1){
					selectedSlot = activeItems;
				}else{
					selectedSlot --;
				}
				Debug.Log(selectedSlot);
			}
		}
	}

	void InitialiseItems(){
		for (int i = 0; i < (content.transform.childCount); i++){
			//transform.GetChild(i).gameObject.SetActive(false);
			//slots[i] = content.transform.GetChild(i + 1).GetComponent<Button>();
			//Debug.Log("executed");
		}
	}

	public void AddItem(Item item){
		slot = content.transform.GetChild(activeItems+1).gameObject;
		slot.SetActive(true);
		slotImage = slot.GetComponent<Slot>().itemImage;
		slotImage.sprite = item.preview;
		activeItems ++;
		selectedSlot = activeItems;
	}

}
