using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class DeathMenu : MonoBehaviour {

	void OnEnable(){
		StartCoroutine(backToMenu());
	}

	IEnumerator backToMenu(){
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene("MainMenu");
	}
}
