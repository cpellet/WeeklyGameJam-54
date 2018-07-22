using UnityEngine.UI;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Image))]
public class CrosshairManager : MonoBehaviour {

	private Camera playerCam;

	public Image crosshairImage;
	public Sprite crosshairNormal;
	public Sprite crosshairGun;
	public Sprite crosshairCollectible;
	public GameManager gm;

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
			actionSlider.value +=.02f;
			yield return new WaitForSeconds(.01f);
			StartCoroutine(executeAction(tag));
		}else{
			actionSlider.gameObject.SetActive(false);
			actionSlider.value = 0;
			StopCoroutine(executeAction(tag));
			yield return null;
		}
	}
	
}
