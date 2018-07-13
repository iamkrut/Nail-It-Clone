using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FreeStarOpenScript : MonoBehaviour {

	public static FreeStarOpenScript instance;

    public GameObject starParticleSystem;

	public GameObject timerPanelStartMenu;
	public TextMeshProUGUI timerTextStartMenu;
	public GameObject timerPanel;
	public TextMeshProUGUI timerText;

	public float freeGiftAfterTime;

	private int starAmount;
	private int finalStarAmount;

	private ulong lastGiftTime;
	private string tempHolder;
	private ulong diff;
	private ulong m;
	private float secondsLeft;
	private string r;
	private bool justOpened;
	private bool isInteractable;

	void Awake(){
		instance = this;
	}

	void OnEnable(){
		justOpened = false;

		if (IsFreeGiftReady()) {
			isInteractable = true;
			timerPanel.SetActive(false);
			timerPanelStartMenu.SetActive(false);
		} else {
			isInteractable = false;
			timerPanel.SetActive(true);
			timerPanelStartMenu.SetActive(true);
		}
	}

	void Update(){
		if (!isInteractable) {
			if (IsFreeGiftReady ()) {
				isInteractable = true;
				timerPanel.SetActive(false);
				timerPanelStartMenu.SetActive(false);				
			} else {
				isInteractable = false;
				timerPanel.SetActive(true);
				timerPanelStartMenu.SetActive(true);
			}
		}
	}

	public bool IsFreeGiftReady(){
		tempHolder = PlayerPrefs.GetString ("LastFreeStarsOpened");
		if (tempHolder != "") {
			isInteractable = true;
			timerPanel.SetActive(false);
			timerPanelStartMenu.SetActive(false);			
			lastGiftTime = ulong.Parse (tempHolder);
		} else {
			lastGiftTime = 0;
		}

		diff = ((ulong) DateTime.Now.Ticks - lastGiftTime);
		m = diff / TimeSpan.TicksPerMillisecond;
		secondsLeft = (float)(freeGiftAfterTime - m) / 1000.0f;
		if (secondsLeft < 0) {
			return true;
		} else {
			r = "";
			//Hours
			secondsLeft -= ((int)secondsLeft / 3600) * 3600;
			//Minutes
			r += ((int) Math.Floor((double)secondsLeft / 60)).ToString ("00");
			//Seconds
			r += ":"+((int) Math.Floor((double)secondsLeft % 60)).ToString ("00");
			isInteractable = false;
			timerText.text = r.ToString ();
			timerTextStartMenu.text = r.ToString ();			
		}

		return false;
	}

	public void OpenGift() {
		if (isInteractable) {
			ClaimStars();
		} else {
			//PNUnityAndroidPlugin.Instance.ShowToast ("Wait for cooldown!");
		}
	}

	public void ClaimStars() {
	 
        //starParticleSystem.SetActive(false);
        //starParticleSystem.SetActive(true);
		justOpened = true;
		isInteractable = false;
		lastGiftTime = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString ("LastFreeStarsOpened", lastGiftTime.ToString ());
		UIManagerScript.instance.DisableGetStarsPanel();
		AudioManagerScript.instance.PlayStarRewardAudio();
		starAmount = PlayerPrefs.GetInt ("StarCount");
		finalStarAmount = starAmount + 100;
		PlayerPrefs.SetInt ("StarCount", finalStarAmount);
        ScoreControllerScript.instance.IncreaseStarValueTo = finalStarAmount;
		ScoreControllerScript.instance.IncreaseStarCount = true;
		
        //ScoreControllerScript.instance.IncreaseStarCount = true;
	}
}
