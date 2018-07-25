using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameCanvasManager : MonoBehaviour {

	public Slider healthSlider;
	public Image[] healthComponents;
	public Text healthLabel;
	public Image damagePanel;
	public Image weaponIndicator;

	public GameObject pauseMenu;
	public GameObject deathMenu;

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
		GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSwitching>().weaponIndicator = weaponIndicator;
		healthSlider.value = 100;
		activeItems = 0;
		selectedSlot = 0;
		damagePanel.enabled = false;
	}

	void Update(){
		if(healthSlider.value <= 0){
			deathMenu.SetActive(true);
		}
		if (activeItems != 0){
			if (Input.GetKeyDown(KeyCode.F1)){
				swithSelection(0);
			}else if (Input.GetKeyDown(KeyCode.F2)){
				swithSelection(1);
			}else if (Input.GetKeyDown(KeyCode.F3)){
				swithSelection(2);
			}else if (Input.GetKeyDown(KeyCode.F4)){
				swithSelection(3);
			}else if (Input.GetKeyDown(KeyCode.F5)){
				swithSelection(5);
			}
		}


		if(Input.GetKeyDown(KeyCode.E)){
			if(slots[selectedSlot].containedItem == null){
				return;
			} 
			if(slots[selectedSlot].containedItem.itemType == Item.type.food){
				if (slots[selectedSlot].amount == 1){
					Regen(slots[selectedSlot].containedItem.healthRegen, slots[selectedSlot].containedItem.name);
					if (healthSlider.value!=100){
						slots[selectedSlot].Initialise();
						slots[selectedSlot].gameObject.SetActive(false);
					}
				}else{
					Regen(slots[selectedSlot].containedItem.healthRegen, slots[selectedSlot].containedItem.name);
					if (healthSlider.value!=100){
						slots[selectedSlot].amount -=1;
						slots[selectedSlot].RefreshAmount();
					}

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
			FindFreeSlots();
			if (freeSlot == 100){
				am.BroadcastMessage(Color.yellow, "Inventory Full");
			}else{
				slots[tempSlot].amount ++;
				slots[tempSlot].RefreshAmount();
			}
		}else{
			FindFreeSlots();
			if (freeSlot == 100){
				am.BroadcastMessage(Color.yellow, "Inventory Full");
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

	public void Regen(int health, string obj){
		if(healthSlider.value == 100){
			am.BroadcastMessage(Color.yellow, "Health already full");
		}else{
			healthSlider.value += health;
			am.BroadcastMessage(Color.green, obj + " : +" + health + " HP");
			CheckHealth();
		}
	}

	public void Damage(){
		StartCoroutine(ShowDamage());
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
