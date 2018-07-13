using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour {

	public static CameraControllerScript instance;

	public Vector3 inGamePosition;
	public Vector3 menuPosition;
	public float timeIntervalToTransit;

	private Vector3 starPosition;
	private Vector3 endPosition;
	private float timeElapsed;
	private bool doTransit;

	void Awake(){
		instance = this;
	}

	void Update(){
		if(doTransit){
			timeElapsed += Time.deltaTime * timeIntervalToTransit;
			transform.position = Vector3.Lerp(starPosition, endPosition, timeElapsed);
			if(timeElapsed == 1){
				doTransit = false;
			}
		}
	}

	public void TransitToMenuPosition(){
		timeElapsed = 0;
		starPosition = inGamePosition;
		endPosition = menuPosition;
		doTransit = true;
	}

	public void TransitToIngamePosition(){
		timeElapsed = 0;
		starPosition = menuPosition;
		endPosition = inGamePosition;
		doTransit = true;
	}
}
