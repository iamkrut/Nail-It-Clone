using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerControllerScript : MonoBehaviour {

	public static HammerControllerScript instance;

	public Vector3 idleAngle;
	public Vector3 strikeAngle;
	public ParticleSystem starParticleSystem;
	public GameObject crackPrefab;
	public GameObject explosionPrefab;

	public float timeIntervalToStrike;

	private float timeElapsed;
	private Quaternion idleAngleQuat;
	private Quaternion strikeAngleQuat;
	private Quaternion fromAngle;
	private Quaternion toAngle;
	private bool doStrike;
	private bool didHit;

	private bool atIdlePosition;
	private bool atStrikePosition;

	void Awake(){
		instance = this;
	}

	void Start(){
		idleAngleQuat = Quaternion.Euler(idleAngle);
		strikeAngleQuat = Quaternion.Euler(strikeAngle);
		
		fromAngle = idleAngleQuat;
		toAngle = strikeAngleQuat;

		atIdlePosition = true;
		atStrikePosition = false;
	}

	void Update () {
		if(doStrike){	
			timeElapsed += Time.deltaTime * timeIntervalToStrike;
			transform.rotation = Quaternion.Lerp(fromAngle, toAngle, timeElapsed);		
			
			if(!atStrikePosition && timeElapsed > 1){
				atStrikePosition = true;
				atIdlePosition = false;
				timeElapsed = 0;
				fromAngle = strikeAngleQuat;
				toAngle = idleAngleQuat; 

				if(!didHit){
					didHit = true;
					GameObject item = Instantiate(crackPrefab, crackPrefab.transform.position, crackPrefab.transform.rotation);
					GameControllerScript.instance.Items.Add(item);
					AudioManagerScript.instance.PlayHammerWoodAudio();
					CameraShakeScript.instance.DoWoodHitShake();
					GameControllerScript.instance.GameOver();
				}

			}else if(!atIdlePosition && timeElapsed > 1){
				atStrikePosition = false;
				atIdlePosition = true;
				timeElapsed = 0;
				doStrike = false;
			}
		}
	}

	public void OnInput(){
		if(atIdlePosition && GameControllerScript.instance.IsGamePlaying){
			timeElapsed = 0;
			fromAngle = idleAngleQuat;
			toAngle = strikeAngleQuat;
			doStrike = true;
			didHit = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if(!didHit){
			CameraShakeScript.instance.DoNormalShake();
			didHit = true;
		
			if (other.gameObject.CompareTag("Nail")) {		
				AudioManagerScript.instance.PlayHammerNailAudio();
				
				if(!other.gameObject.GetComponent<NailControllerScript>().IsPunched) {
					ScoreControllerScript.instance.AddScore();
					other.gameObject.GetComponent<NailControllerScript>().PunchNail();
				}
			} else if (other.gameObject.CompareTag("Star")) {
				starParticleSystem.gameObject.transform.position = other.gameObject.transform.position;
				starParticleSystem.Play();
				ScoreControllerScript.instance.AddScore();
				ScoreControllerScript.instance.AddStar();
				AudioManagerScript.instance.PlayStarCollectAudio();
				Destroy(other.gameObject);
			} else if (other.gameObject.CompareTag("Bomb")) {
				Destroy(other.gameObject);
				AudioManagerScript.instance.PlayBombExplosionAudio();
				Instantiate(explosionPrefab, explosionPrefab.transform.position, explosionPrefab.transform.rotation);
				GameControllerScript.instance.GameOver();
				CameraShakeScript.instance.DoBombShake();
			} else if (other.gameObject.CompareTag("PowerUpStarFrenzy")) {
				Destroy(other.gameObject);
				AudioManagerScript.instance.PlayStarCollectAudio();
				ScoreControllerScript.instance.AddScore();
				PowerUpManagerScript.instance.IsStarFrenzy = true;
			} else if (other.gameObject.CompareTag("PowerUp2xScoreMultiplier")) {
				Destroy(other.gameObject);
				AudioManagerScript.instance.PlayStarCollectAudio();
				ScoreControllerScript.instance.AddScore();
				PowerUpManagerScript.instance.Is2xMultiplier = true;
			} else if (other.gameObject.CompareTag("PowerUpSlowMotion")) {
				Destroy(other.gameObject);
				AudioManagerScript.instance.PlayStarCollectAudio();
				ScoreControllerScript.instance.AddScore();
				PowerUpManagerScript.instance.IsSlowMotion = true;
			}
		}
	}
}
