using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameControllerScript : MonoBehaviour {

	public static GameControllerScript instance;

	public GameObject nailPrefab;
	public GameObject starPrefab;
	public GameObject bombPrefab;
	public GameObject powerUpStarFrenzyPrefab;
	public GameObject powerUp2xScoreMultiplayerPrefab;
	public GameObject powerUpSlowMotionPrefab;

	public float minSpawnTimeInterval;
	public float maxSpawnTimeInterval;	
	public float spawnTimeInterval;
	public GameObject hammerHolder;

	public Transform itemSpawnPosition;

	private List<GameObject> items = new List<GameObject>();

	private bool spawnItems;
	private float lastSpawnTime;
	private bool isGamePlaying;

	private bool isScoreMultiplayer;
	private bool isStarFrenzy;
	private bool isSlowMotion;

	public bool IsGamePlaying { set { isGamePlaying = value; } get { return isGamePlaying; } }
	public bool IsScoreMultiplayer { get { return isScoreMultiplayer; } } 
	public bool IsStarFrenzy { get { return isStarFrenzy; } } 	
	public bool IsSlowMotion { get { return isSlowMotion; } } 	
	public List<GameObject> Items { get { return items; } }

	void Awake(){
		instance = this;
	}

	void Start(){
		spawnItems = false;

		if(PlayerPrefs.GetInt("Restart") == 1){
			PlayerPrefs.SetInt("Restart", 0);
			UIManagerScript.instance.OnPlay();
		}
	}

	void Update(){
		if(spawnItems && Time.time >= lastSpawnTime + spawnTimeInterval){
			lastSpawnTime = Time.time;
			spawnTimeInterval = Random.Range(minSpawnTimeInterval, maxSpawnTimeInterval);

			if(isSlowMotion){
				spawnTimeInterval = Random.Range(minSpawnTimeInterval + 0.2f, maxSpawnTimeInterval + 0.2f);				
			}

			GameObject item;
			if(isStarFrenzy){
				item = Instantiate(starPrefab, new Vector3(starPrefab.transform.position.x, starPrefab.transform.position.y, itemSpawnPosition.position.z), Quaternion.identity) as GameObject;						
			}else{
				if(Random.Range(0, 10) >= 2){
					item = Instantiate(nailPrefab, new Vector3(nailPrefab.transform.position.x, nailPrefab.transform.position.y, itemSpawnPosition.position.z), Quaternion.identity) as GameObject;
				}else {
					switch (Random.Range(0,15))
					{
						case 0:
						case 2:
						case 10:
						case 5:
						case 7:
						case 9:
							item = Instantiate(bombPrefab, new Vector3(bombPrefab.transform.position.x, bombPrefab.transform.position.y, itemSpawnPosition.position.z), Quaternion.identity) as GameObject;						
							break;
						case 1:
						case 3:
						case 11:
						case 12:
						case 13:
						case 14:
						case 15:
							item = Instantiate(starPrefab, new Vector3(starPrefab.transform.position.x, starPrefab.transform.position.y, itemSpawnPosition.position.z), Quaternion.identity) as GameObject;						
							break;
						case 4:
							item = Instantiate(powerUpStarFrenzyPrefab, new Vector3(powerUpStarFrenzyPrefab.transform.position.x, powerUpStarFrenzyPrefab.transform.position.y, itemSpawnPosition.position.z), Quaternion.identity) as GameObject;						
							break;					
						case 6:
							item = Instantiate(powerUp2xScoreMultiplayerPrefab, new Vector3(powerUp2xScoreMultiplayerPrefab.transform.position.x, powerUp2xScoreMultiplayerPrefab.transform.position.y, itemSpawnPosition.position.z), Quaternion.identity) as GameObject;						
							break;				
						case 8:
							item = Instantiate(powerUpSlowMotionPrefab, new Vector3(powerUpSlowMotionPrefab.transform.position.x, powerUpSlowMotionPrefab.transform.position.y, itemSpawnPosition.position.z), Quaternion.identity) as GameObject;
							break;				
						default:
							item = Instantiate(starPrefab, new Vector3(starPrefab.transform.position.x, starPrefab.transform.position.y, itemSpawnPosition.position.z), Quaternion.identity) as GameObject;						
							break;
					}
				}
			}
			items.Add(item);
		}
	}

	public void StartGame(){
		GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Nail It");
		isGamePlaying = true;
		spawnItems = true;
		items.Clear();
	}

	public void GameOver(){
		if(isGamePlaying){
			GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Nail It", ScoreControllerScript.instance.Score);
			isGamePlaying = false;
			StartCoroutine(WaitBeforeGameOver());
			PowerUpManagerScript.instance.DisableAllPowerUps();
			DisableAllPowerUps();
			ScoreControllerScript.instance.UpdateHighScore();
		}
	}

	IEnumerator WaitBeforeGameOver(){
		yield return new WaitForSeconds(0.3f);
		hammerHolder.SetActive(false);
		UIManagerScript.instance.OnGameOver();
	}

	public void EnableStarFrenzy (){
		isStarFrenzy = true;
	}

	public void Enable2xMultiplier (){
		isScoreMultiplayer = true;
	}

	public void EnableSlowMotion (){
		isSlowMotion = true;
		ChangeSpeedOfItems();
	}

	public void DisableStarFrenzy (){
		isStarFrenzy = false;
	}

	public void Disable2xMultiplier (){
		isScoreMultiplayer = false;
	}

	public void DisableSlowMotion (){
		isSlowMotion = false;
		ChangeSpeedOfItems();
	}

	public void ChangeSpeedOfItems(){
		float value = 5;
		spawnTimeInterval = 0.7f;
		if(isSlowMotion){
			value = 3;
			spawnTimeInterval = 1;
		}

		foreach (GameObject item in items)
		{
			if(item != null){
				item.GetComponent<MoverScript>().MovementSpeed = value;
			}
		}
	}

	public void DisableAllPowerUps(){
		Disable2xMultiplier();
		DisableStarFrenzy();
		DisableSlowMotion();
	}
}
