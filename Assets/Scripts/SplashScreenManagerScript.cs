using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SplashScreenManagerScript : MonoBehaviour {

	public float time;

	private ulong lastTimePlayed;
	private string tempHolder;
	private ulong diff;
	private ulong m;

	void Awake(){
		Application.targetFrameRate = 60;
	}

	void Start () {
		StartCoroutine (WaitBeforeScreenChange ());
    }

    IEnumerator WaitBeforeScreenChange(){
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene (1);
	}
}