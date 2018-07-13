using UnityEngine; 
using System.Collections;

public class CameraShakeScript : MonoBehaviour { 

	public static CameraShakeScript instance;

	public bool shakePosition;
	public bool shakeRotation;

	public float shakeIntensity = 0.5f; 
	public float shakeDecay = 0.02f;

	private Vector3 OriginalPos;
	private Quaternion OriginalRot;

	private bool isShakeRunning = false;
	private float currentShakeIntensity;

	public float ShakeIntensity {
		set{
			shakeIntensity = value;
		}
		get{
			return shakeIntensity;
		}
	}

	public float ShakeDecay {
		set{
			shakeDecay = value;
		}
		get{
			return shakeDecay;
		}
	}

	void Awake(){
		instance = this;
	}

	void Start(){
		OriginalPos = CameraControllerScript.instance.inGamePosition;
		OriginalRot = transform.rotation;
	}

	public void DoNailLeftShake(){
		shakeIntensity = 0.5f;
		shakeDecay = 0.02f;
		
		StartCoroutine ("ProcessShake");
	}

	public void DoBombShake(){
		shakeIntensity = 1.0f;
		shakeDecay = 0.01f;
		
		StartCoroutine ("ProcessShake");
	}

	public void DoWoodHitShake(){
		shakeIntensity = 0.2f;
		shakeDecay = 0.01f;
		
		StartCoroutine ("ProcessShake");
	}

	public void DoNormalShake()
	{	
		shakeIntensity = 0.12f;
		shakeDecay = 0.01f;
		//OriginalPos = transform.position;
		//OriginalRot = transform.rotation;

		StartCoroutine ("ProcessShake");
	}

	IEnumerator ProcessShake()
	{
		if (!isShakeRunning || isShakeRunning) {
			isShakeRunning = true;
			currentShakeIntensity = shakeIntensity;

			while (currentShakeIntensity > 0) {
				if (shakePosition) {
					transform.position = OriginalPos + Random.insideUnitSphere * currentShakeIntensity;
				}
				if (shakeRotation) {
					transform.rotation = new Quaternion (OriginalRot.x + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f,
						OriginalRot.y + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f,
						OriginalRot.z + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f,
						OriginalRot.w + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f);
				}
				currentShakeIntensity -= shakeDecay;
				yield return null;
			}

			transform.position = OriginalPos;
			isShakeRunning = false;
		}
	}
}