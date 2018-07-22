using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public Transform spawnPoint;
	public Camera playerCam;
	private GameObject player;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		player = GameObject.Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
		playerCam = player.GetComponentInChildren<Camera>();
	}
}