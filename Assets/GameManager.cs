using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public Transform spawnPoint;

	// Use this for initialization
	void Start () {
		GameObject.Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
	}
}
