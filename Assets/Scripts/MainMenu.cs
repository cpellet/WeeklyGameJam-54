using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public GameObject creditCanvas;
	public GameObject mainMenu;

	public void StartGame(){
		SceneManager.LoadSceneAsync("LoadingScene");
	}

	public void ShowCredits(){
		mainMenu.GetComponent<Canvas>().enabled = false;
		creditCanvas.SetActive(true);
	}

	public void HideCredits(){
		creditCanvas.SetActive(false);
		mainMenu.GetComponent<Canvas>().enabled = true;
	}

	public void Quit(){
		Application.Quit();
	}
}
