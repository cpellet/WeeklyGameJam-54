using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public void LoadWeapon(Item item, Camera cam){
		GameObject weapon = Instantiate(item.prefab, gameObject.transform.position, gameObject.transform.rotation);
		weapon.transform.SetParent(gameObject.transform);
		weapon.GetComponent<gun>().fpsCam = cam;
	}

	public void UnloadWeapon(){
		Destroy(gameObject.GetComponentInChildren<gun>().gameObject);
	}
}
