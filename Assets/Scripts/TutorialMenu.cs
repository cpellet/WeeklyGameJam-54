using System.Collections;
using UnityEngine;

public class TutorialMenu : MonoBehaviour {

	void OnEnable(){
		StartCoroutine(hideTutorial());
	}

	IEnumerator hideTutorial(){
		yield return new WaitForSeconds (10);
		gameObject.SetActive(false);
		StopAllCoroutines();
	}
}
