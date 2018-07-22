using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour {

	private GameObject slot;
	public Image itemImage;
	[HideInInspector]
	public Item containedItem;
	[HideInInspector]
	public bool isOccupied;
	[HideInInspector]
	public bool isSelected;
	public Animator anim;
	[HideInInspector]
	public int amount;
	public Text amountText;

	void Start(){
		Initialise();
	}

	public void Initialise(){
		slot = this.gameObject;
		slot.SetActive(false);
		amount = 0;
		RefreshAmount();
		isOccupied = false;
		isSelected = false;
	}

	public void Populate(Item item){
		slot.SetActive(true);
		isOccupied = true;
		amount = 1;
		RefreshAmount();
		containedItem = item;
		itemImage.sprite = containedItem.preview;
	}

	public void RefreshAmount(){
		amountText.text = amount.ToString();
	}

	public void ToggleSelect(){
		if (isSelected == false){
			isSelected = true;
			anim.Play("Highlighted");
		}else if (isSelected == true){
			isSelected = false;
			anim.Play("Normal");
		}
	}
}
