using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameCanvasManager : MonoBehaviour {

	public Slider healthSlider;
	public Image[] healthComponents;
	public Text healthLabel;
	public Image damagePanel;

	public GameObject pauseMenu;

	private int selectedSlot;
	private int activeItems;
	public Slot[] slots;
	private int freeSlot;
	private int tempSlot;

	public float minSurviveFall = 1f;
    public int damageForSeconds;
    private RigidbodyFirstPersonController _controller;
    private float airTime = 0;

	public AnnouncerManager am;

	void Start(){
		_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<RigidbodyFirstPersonController>();
		healthSlider.value = 100;
		activeItems = 0;
		selectedSlot = 0;
		damagePanel.enabled = false;
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
			}
		}

		if(Input.GetKeyDown(KeyCode.E)){
			if(slots[selectedSlot].containedItem == null){
				return;
			} 
			if(slots[selectedSlot].containedItem.itemType == Item.type.food){
				if (slots[selectedSlot].amount == 1){
					Regen(slots[selectedSlot].containedItem.healthRegen);
					am.BroadcastMessage(Color.green, (slots[selectedSlot].containedItem.name + " : + " + slots[selectedSlot].containedItem.healthRegen.ToString() + " HP"));
					slots[selectedSlot].Initialise();
					slots[selectedSlot].gameObject.SetActive(false);
				}else{
					Regen(slots[selectedSlot].containedItem.healthRegen);
					am.BroadcastMessage(Color.green, (slots[selectedSlot].containedItem.name + " : + " + slots[selectedSlot].containedItem.healthRegen.ToString() + " HP"));
					slots[selectedSlot].amount -=1;
					slots[selectedSlot].RefreshAmount();
				}
			}
		}

		if(!_controller.Grounded){
 			airTime += Time.deltaTime;
 		}
 		if(_controller .Grounded){
 			if(airTime > minSurviveFall){
				damageForSeconds = 18;
 				healthSlider.value -= damageForSeconds * airTime;
				CheckHealth();
				StartCoroutine(ShowDamage());
 			}
 		airTime = 0;
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
			if(slots[i].containedItem != null){
				if(slots[i].containedItem.name == item.name){
					tempSlot = i;
					break;
				}
			}
		}
	}

	void CheckHealth(){
		if (healthSlider.value <= 30){
			for (int i = 0; i < healthComponents.Length; i++){
				healthComponents[i].color = Color.red;
			}
			healthLabel.color = Color.red;
		}else{
			for (int i = 0; i < healthComponents.Length; i++){
				healthComponents[i].color = Color.white;
			}
			healthLabel.color = Color.white;
		}
	}

	public void Regen(int health){
		healthSlider.value += health;
		CheckHealth();
	}

	public IEnumerator ShowDamage(){
		damagePanel.enabled = true;
		damagePanel.gameObject.GetComponent<Animator>().enabled = true;
		yield return new WaitForSeconds(2);
		damagePanel.enabled = false;
		damagePanel.gameObject.GetComponent<Animator>().enabled = false;
		damagePanel.gameObject.GetComponent<Animator>().Rebind();
		StopCoroutine(ShowDamage());
	}
}
