using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarControllerScript : MonoBehaviour {

	public float rotationSpeed;

	private GameObject starMeshObject;

	void Start(){
		starMeshObject = transform.GetChild(0).gameObject;
	}

	void Update(){
		starMeshObject.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
	}
}
