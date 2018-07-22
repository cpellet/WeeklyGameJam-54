using UnityEngine.UI;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour {

	public Slider healthSlider;
	public Slider memoryRecoveredSlider;
	public Item[] playerItems;
	private Image slotImage;
	private int selectedSlot;
	private int activeItems;
	public Slot[] slots;
	private int freeSlot;
	private int tempSlot;

	void Start(){
		healthSlider.value = 100;
		memoryRecoveredSlider.value = 0;
		activeItems = 0;
		selectedSlot = 0;
	}

	void Update(){
		if (activeItems != 0){
			if (Input.GetAxis("Mouse ScrollWheel") > 0f){
				if(selectedSlot == activeItems - 1){
					swithSelection(0);
				}else{
					swithSelection(selectedSlot+1);
				}
				Debug.Log(selectedSlot);
			}else if (Input.GetAxis("Mouse ScrollWheel") < 0f){
				if (selectedSlot == 0){
					swithSelection(activeItems-1);
				}else{
					swithSelection(selectedSlot-1);
				}
				Debug.Log(selectedSlot);
			}
		}
	}

	public void AddItem(Item item){
		CheckIfAlreadyOwned(item);
		if (tempSlot != 100){
			slots[tempSlot].amount ++;
			slots[tempSlot].RefreshAmount();
		}else{
			FindFreeSlots();
			if (freeSlot == 100){
				Debug.Log("Inventory Full");
			}else{
				if (activeItems == 0){
					activeItems++;
					slots[freeSlot].Populate(item);
					slots[freeSlot].ToggleSelect();
					selectedSlot = freeSlot;
				}else{
					activeItems++;
					slots[freeSlot].Populate(item);
					swithSelection(freeSlot);
				}
			}
		}
	}

	void swithSelection(int newSlot){
		slots[selectedSlot].ToggleSelect();
		slots[newSlot].ToggleSelect();
		selectedSlot = newSlot;
	}

	void FindFreeSlots(){
		freeSlot = 100;
		for (int i = 0; i < slots.Length; i++){
			if (slots[i].isOccupied == false){
				freeSlot = i;
				break;
			}
		}
	}

	void CheckIfAlreadyOwned(Item item){
		tempSlot = 100;
		for (int i = 0; i < slots.Length; i++){
			if(slots[i].containedItem == item){
				tempSlot = i;
				break;
			}
		}
	}
}
