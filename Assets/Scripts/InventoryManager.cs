using UnityEngine.UI;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Image))]
public class InventoryManager : MonoBehaviour {

	private Camera playerCam;
	private CollectibleItem itemToPickup;

	public Image crosshairImage;
	public Sprite crosshairNormal;
	public Sprite crosshairGun;
	public Sprite crosshairCollectible;
	public Text collectionText;

	public GameManager gm;
	public GameCanvasManager gcm;

	public Slider actionSlider;

	RaycastHit hit;

	void Start(){
		playerCam = gm.playerCam;
		crosshairImage = GetComponent<Image>();
		crosshairImage.sprite = crosshairNormal;
		actionSlider.value = 0;
		actionSlider.gameObject.SetActive(false);
	}
	void Update(){
		if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit)){
			if (hit.transform.tag == "Collectibles"){
				crosshairImage.sprite = crosshairCollectible;
				if (Input.GetMouseButtonDown(0)){
					if (Vector3.Distance(playerCam.transform.position, hit.transform.position) < 2){
						StartCoroutine(executeAction(hit.transform.tag));
					}
				}
			}else{
				crosshairImage.sprite = crosshairNormal;
			}
		}
	}

	public IEnumerator executeAction(string tag){
		actionSlider.gameObject.SetActive(true);
		if (actionSlider.value != 1 && hit.transform.tag == tag){
			collectionText.text = "Collecting " + hit.transform.gameObject.GetComponent<CollectibleItem>().item.name;
			actionSlider.value +=.02f;
			yield return new WaitForSeconds(.01f);
			StartCoroutine(executeAction(tag));
		}else if (actionSlider.value == 1 &&hit.transform.tag == tag){
			pickup();
			actionSlider.gameObject.SetActive(false);
			actionSlider.value = 0;
			StopCoroutine(executeAction(tag));
			yield return null;
		}else{
			actionSlider.gameObject.SetActive(false);
			actionSlider.value = 0;
			StopCoroutine(executeAction(tag));
			yield return null;
		}
	}

	public void pickup(){
		itemToPickup = hit.transform.gameObject.GetComponent<CollectibleItem>();
		gcm.AddItem(itemToPickup.item);
	}
	
}
