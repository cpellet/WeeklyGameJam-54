using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public Transform spawnPoint;
	public Camera playerCam;
	private GameObject player;
	public Image weaponIndicator;
	public static bool canShoot;
	public Transform sculpture;
	public GameCanvasManager gcm;

	// Use this for initialization
	void Start () {
		canShoot=true;
		DontDestroyOnLoad(gameObject);
		player = GameObject.Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
		playerCam = player.GetComponentInChildren<Camera>();
	}
}