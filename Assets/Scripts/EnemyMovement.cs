using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public Transform target;
	public int speed;
	public GameManager gm;
	private bool hasShownDamage = false;

	void Start(){
		gm = FindObjectOfType<GameManager>();
		target = gm.sculpture;
	}

	void Update(){
		float step = speed * Time.deltaTime;

		if(Vector3.Distance(transform.position, target.position) <= 15){
			transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
			gm.gcm.healthSlider.value-=1;
			if (!hasShownDamage){
				gm.gcm.Damage();
				hasShownDamage = true;
			}

		}else{
			transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		}
	}
}
