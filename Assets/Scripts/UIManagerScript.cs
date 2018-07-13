using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {

	public static UIManagerScript instance;

	public GameObject gameOverPanel;
	public GameObject startGamePanel;
	public GameObject ingamePanel;
	public GameObject touchPadPanel;
	public GameObject getStarPanel;

	void Awake(){
		instance = this;
	}

	public void OnGameOver(){
		gameOverPanel.SetActive(true);
	}

	public void OnHome(){
		SceneManager.LoadScene(1);		
	}

	public void OnRestart(){
		PlayerPrefs.SetInt("Restart", 1);
		SceneManager.LoadScene(1);
	}

	public void OnGetStars(){
		getStarPanel.SetActive(true);
	}

	public void OnPlay(){
		CameraControllerScript.instance.TransitToIngamePosition();
		touchPadPanel.SetActive(true);
		ingamePanel.SetActive(true);
		startGamePanel.SetActive(false);
		GameControllerScript.instance.StartGame();
	}

	public void OnContinue(){
		if(PlayerPrefs.GetInt("StarCount") >= 80){
		
		}else{
			OnGetStars();
		}
	}

	public void DisableGetStarsPanel(){
		getStarPanel.GetComponent<Animator>().SetTrigger("FadeOut");
	}
}
