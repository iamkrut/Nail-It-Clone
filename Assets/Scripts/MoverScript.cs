using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverScript : MonoBehaviour {

	public float movementSpeed;
	
	public float MovementSpeed { set { movementSpeed = value; } }

	void Awake(){
		if(GameControllerScript.instance.IsSlowMotion){
			movementSpeed = 3;
		}
	}

	void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
	}
}
