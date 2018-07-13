using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailControllerScript : MonoBehaviour {

	public Vector3 nailSpawnPosition;
	public Vector3 nailPunchPosition;
	public float timeIntervalToPunch;

	private float timeElapsed;
	private bool doPunchNail;
	private bool isPunched;

	public bool IsPunched { set { isPunched = value; } get { return isPunched; } }

	void OnEnable(){
		isPunched = false;
		doPunchNail = false;
	}

	void Update(){
		if(doPunchNail){
			timeElapsed += Time.deltaTime * timeIntervalToPunch;
			
			transform.position = Vector3.Lerp(new Vector3(transform.position.x, nailSpawnPosition.y, transform.position.z), new Vector3(transform.position.x, nailPunchPosition.y, transform.position.z), timeElapsed);
			
			if(timeElapsed > 1){
				doPunchNail = false;
			}
		}

		if(!isPunched && transform.position.z > 2 && GameControllerScript.instance.IsGamePlaying){
			CameraShakeScript.instance.DoNailLeftShake();
			GameControllerScript.instance.GameOver();
		}
	}

	public void PunchNail(){
		if(!isPunched){
			timeElapsed = 0;
			doPunchNail = true;
			isPunched = true;
		}
	}
}
