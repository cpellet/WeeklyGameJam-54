using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	void OnEnable(){
		StartCoroutine(stopTime());
	}

	IEnumerator stopTime(){
		yield return new WaitForSeconds(.1f);
		Time.timeScale = 0f;
		StopAllCoroutines();
	}

	public void Continue(){
		Time.timeScale = 1f;
		this.gameObject.SetActive(false);
	}

	public void RestartLevel(){
		SceneManager.LoadScene("LoadingScene");
	}
}
