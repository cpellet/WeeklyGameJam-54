using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoading : MonoBehaviour {

	public Slider loadingBar;

	void Start () {
		StartCoroutine(LoadLevelAsynchronously(2));
	}

	IEnumerator LoadLevelAsynchronously(int sceneIndex){
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		while (!operation.isDone){
			float progress = Mathf.Clamp01(operation.progress / .9f);
			loadingBar.value = progress;
			yield return null;
		}
	}
	

}
