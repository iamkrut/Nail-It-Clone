using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreControllerScript : MonoBehaviour {

	public static ScoreControllerScript instance;

	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI starText;	
	public TextMeshProUGUI startMenuHighscoreText;
	public TextMeshProUGUI gameoverMenuHighscoreText;

	public TextMeshProUGUI startMenuStarText;

	private int score;
	private int starCount;
	private Animator scoreTextAnimator;
	private Animator starTextAnimator;

	private bool increaseStarCount;
    private int increaseStarValueTo;
    private int previousStarValue;
    private float starValueTime;
    public float starValueIncrementBy;

	public int Score { get { return score; } }

	public int IncreaseStarValueTo {
        set { increaseStarValueTo = value; }
    }

    public bool IncreaseStarCount {
        set {
            increaseStarCount = value;
            starValueTime = 0;
            previousStarValue = starCount;
        }
    }

	void Awake(){
		instance = this;
	}

	void Start () {
		scoreTextAnimator = scoreText.gameObject.GetComponent<Animator>();
		starTextAnimator = starText.gameObject.GetComponent<Animator>();
		score = 0;
		starCount = PlayerPrefs.GetInt("StarCount");
		startMenuHighscoreText.text = "BEST: " + PlayerPrefs.GetInt("HighScore");
		gameoverMenuHighscoreText.text = "BEST: " + PlayerPrefs.GetInt("HighScore");
		UpdateScoreText();
		UpdateStarCountText();
	}

	void Update() {
		if (increaseStarCount)
        {
            starCount = (int)Mathf.Ceil(Mathf.Lerp(previousStarValue, increaseStarValueTo, starValueTime));
            starValueTime += Time.unscaledDeltaTime * starValueIncrementBy;
            UpdateStarCountText();
			UpdateOtherGUIStarText();

            if (starValueTime >= 1)
            {
                increaseStarCount = false;
            }
        }
	}

	public void AddScore(){
		score++;
		if(GameControllerScript.instance.IsScoreMultiplayer){
			score++;
		}
		UpdateScoreText();
	}

	public void AddStar(){
		starCount++;
		UpdateStarCountText();
	}

	void UpdateScoreText(){
		scoreText.text = score.ToString();
		scoreTextAnimator.SetTrigger("OnScore");
	}

	void UpdateStarCountText(){
		starText.text = starCount.ToString();
		starTextAnimator.SetTrigger("OnScore");
	}

	void UpdateOtherGUIStarText(){
		startMenuStarText.text = starCount.ToString();
	}

	public void UpdateHighScore(){
		if(PlayerPrefs.GetInt("HighScore") < score){
			PlayerPrefs.SetInt("HighScore", score);
			startMenuHighscoreText.text = "BEST: " + score;
			gameoverMenuHighscoreText.text = "BEST: " + score;
		}
	}
}
