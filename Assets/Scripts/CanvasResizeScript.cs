using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasResizeScript : MonoBehaviour {

	public CanvasScaler canvasScaler;

	void Awake(){

		canvasScaler = GetComponent<CanvasScaler>();
		
		if (System.Math.Round(Camera.main.aspect, 2) == (float) 3/4) {
			canvasScaler.referenceResolution = new Vector2(1024, 768);
		} else if(Camera.main.aspect == (float) 2/3) {
			canvasScaler.referenceResolution = new Vector2(960, 640);
		} else {
			canvasScaler.referenceResolution = new Vector2(800, 600);			
		}
	}
}
