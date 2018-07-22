using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class CrosshairManager : MonoBehaviour {

	private Camera playerCam;

	public Image crosshairImage;
	public Sprite crosshairNormal;
	public Sprite crosshairGun;
	public Sprite crosshairCollectible;
	public GameManager gm;

	public RaycastHit hit;
	public Ray ray;

	void Start(){
		playerCam = gm.playerCam;
		crosshairImage = GetComponent<Image>();
		crosshairImage.sprite = crosshairNormal;
	}
	void Update(){
		ray = playerCam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)){
			Debug.Log("HITT");
			if (hit.transform.gameObject.tag == "Collectible"){
				crosshairImage.sprite = crosshairCollectible;
			}
		}
	}
	
}
