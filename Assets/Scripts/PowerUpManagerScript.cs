using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpManagerScript : MonoBehaviour {

	public static PowerUpManagerScript instance;

	public float starFrenzyTime;
	public float score2xMultiplierTime;
	public float slowMotionTime;
    public TextMeshProUGUI powerupText1;
	public TextMeshProUGUI powerupText2;
    public GameObject powerupTextPanel;

	private bool isStarFrenzy;
	private bool is2xMultiplier;
	private bool isSlowMotion;

	private float starFrenzyPickupTime;
	private float score2xMultiplierPickupTime;
	private float slowMotionPickupTime;
		
	public GameObject starFrenzyIndicatorHolder;
	public GameObject score2xMultiplierIndicatorHolder;
	public GameObject slowMotionIndicatorHolder;

	public GameObject starFrenzyIndicator;
	public GameObject score2xMultiplierIndicator;
	public GameObject slowMotionIndicator;

    public bool IsStarFrenzy { 
		set { 
			if (!isStarFrenzy) { // if its not ongoing
				isStarFrenzy = value;
				if (value == true) {
					GameControllerScript.instance.EnableStarFrenzy ();
					starFrenzyIndicatorHolder.SetActive (true);

                    powerupTextPanel.SetActive(false);
                    powerupText1.text = "star";
                    powerupText2.text = "frenzy";
                    powerupTextPanel.SetActive(true);
                }
            }
			starFrenzyPickupTime = Time.time;
        } 
		get { 
			return isStarFrenzy; 
		} 
	}

	public bool Is2xMultiplier { 
		set { 
			if (!is2xMultiplier) { // if 2x multiplier is not ongoing
				is2xMultiplier = value; 
				if (value == true) {
					GameControllerScript.instance.Enable2xMultiplier ();
					score2xMultiplierIndicatorHolder.SetActive (true);

                    powerupTextPanel.SetActive(false);
                    powerupText1.text = "2x";
                    powerupText2.text = "multiplier";
                    powerupTextPanel.SetActive(true);
                }
			}
			score2xMultiplierPickupTime = Time.time;
		} 
		get { 
			return is2xMultiplier; 
		} 
	}

	public bool IsSlowMotion { 
		set { 
			if (!isSlowMotion) {
				isSlowMotion = value; 
				if (value == true) {
					GameControllerScript.instance.EnableSlowMotion ();
					slowMotionIndicatorHolder.SetActive (true);

                    powerupTextPanel.SetActive(false);
                    powerupText1.text = "slow";
                    powerupText2.text = "motion";
                    powerupTextPanel.SetActive(true);
                }
            }
			slowMotionPickupTime = Time.time;
        } 
		get { 
			return isSlowMotion; 
		} 
	}

	void Awake(){
		instance = this;
	}

	void Update(){
		if (isStarFrenzy) {
			starFrenzyIndicator.GetComponent<Image> ().fillAmount = (starFrenzyTime * Time.timeScale - (Time.time - starFrenzyPickupTime))  * 0.1f / Time.timeScale;
		}
		if (is2xMultiplier) {
			score2xMultiplierIndicator.GetComponent<Image> ().fillAmount = (score2xMultiplierTime * Time.timeScale - (Time.time - score2xMultiplierPickupTime)) * 0.1f / Time.timeScale;
		}
		if (isSlowMotion) {
			slowMotionIndicator.GetComponent<Image> ().fillAmount = (slowMotionTime * Time.timeScale - (Time.time - slowMotionPickupTime)) * 0.1f / Time.timeScale;
		}

		if (isStarFrenzy && Time.time >= starFrenzyTime * Time.timeScale + starFrenzyPickupTime) {
			isStarFrenzy = false;
			GameControllerScript.instance.DisableStarFrenzy ();
			starFrenzyIndicatorHolder.SetActive (false);
		}
		if ((is2xMultiplier) && Time.time >= score2xMultiplierTime * Time.timeScale + score2xMultiplierPickupTime) {
			is2xMultiplier = false;
			GameControllerScript.instance.Disable2xMultiplier ();
			score2xMultiplierIndicatorHolder.SetActive (false);
		}
		if (isSlowMotion && Time.time >= slowMotionTime * Time.timeScale + slowMotionPickupTime) {
			isSlowMotion = false;
			GameControllerScript.instance.DisableSlowMotion();
			slowMotionIndicatorHolder.SetActive (false);
		}
	}

	public void DisableAllPowerUps(){
		IsStarFrenzy = false;
		Is2xMultiplier = false;
		IsSlowMotion = false;

		starFrenzyIndicatorHolder.SetActive (false);
		score2xMultiplierIndicatorHolder.SetActive (false);
		slowMotionIndicatorHolder.SetActive (false);
		
		powerupTextPanel.SetActive(false);
	}

    public bool IsAnyPowerUpActive() {
        return IsStarFrenzy || Is2xMultiplier || IsSlowMotion;
    }
}   
