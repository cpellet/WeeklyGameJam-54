﻿using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {

	public float damage = 10f;
	public float range = 100f;
	public float impactForce = 30f;
	public float fireRate = 15f
	;
	public Camera fpsCam;
	public GameObject impactEffect;

	private float nextTimeToFire = 0f;

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0) && Time.time >= nextTimeToFire){
			nextTimeToFire = Time.time + 1f/fireRate;
			Shoot();
		}
	}

	void Shoot(){
		RaycastHit hit;
		if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
			target target = hit.transform.GetComponent<target>();
			if(target!=null){
				target.TakeDamage(damage);
			}
			if(hit.rigidbody!=null){
				hit.rigidbody.AddForce(-hit.normal*impactForce);
			}
			GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(impactGO, 2);
		}
	}

}
