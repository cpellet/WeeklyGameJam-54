using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DayNightController))]
public class SpawnManager : MonoBehaviour {

	public Transform[]spawnPoints;
	public GameObject[] objects;
	public Transform enemySpawnPoint;
	public GameObject enemyPrefab;
	private int random;
	private DayNightController dnc;
	private int timeRemainingForSpawn;
	private bool hasInvoked = false;

	void Start(){
		dnc = GetComponent<DayNightController>();
		for (int i = 0; i < spawnPoints.Length; i++){
			random = Random.Range(0, objects.Length);
			GameObject spawnedItem = Instantiate(objects[random], spawnPoints[i].position, spawnPoints[i].rotation);
		}
	}
	void Update(){
		if (dnc.currentTimeOfDay<0.2 || dnc.currentTimeOfDay > 0.8){
			if(!hasInvoked){
				InvokeRepeating("SpawnEnemy", 0, Random.Range(1f, 10f));
				hasInvoked = true;
			}
		}else{
			CancelInvoke();
			hasInvoked = false;
		}
	}

	void SpawnEnemy(){
		GameObject enemyClone = Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
	}
}
