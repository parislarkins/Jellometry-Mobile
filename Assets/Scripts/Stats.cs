using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

	private int totalEnemiesKilled;
	public int gruntsKilled;
	public int tanksKilled;
	public int archersKilled;
	
	private GameObject deadCanvas;
	public GameObject highscoreObj;
	public GameObject scoreObj;
	public GameObject gruntsObj;
	public GameObject tanksObj;
	public GameObject archersObj;
	
	private Text highscoreText;
	private Text scoreText;
	private Text gruntsText;
	private Text tanksText;
	private Text archersText;
	
	private ScoreManager scoreManager;
	
    // New Code
    public Menu canvasPlayMenu;
    public Menu canvasEndMenu;
    //**


    public GameObject canvasPlay;
	public GameObject canvasEnd;
	public GameObject controlSticks;

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("totalEnemiesKilled"))
			PlayerPrefs.SetInt("totalEnemiesKilled", 0);

		totalEnemiesKilled = PlayerPrefs.GetInt("totalEnemiesKilled");
	
		scoreManager = GameObject.Find("Player").GetComponent<ScoreManager>();
		
		highscoreText = highscoreObj.GetComponent<Text>();
		scoreText = scoreObj.GetComponent<Text>();
		gruntsText = gruntsObj.GetComponent<Text>();
		tanksText = tanksObj.GetComponent<Text>();
		archersText = archersObj.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void SaveStats(){
		totalEnemiesKilled += gruntsKilled + tanksKilled + archersKilled;
		PlayerPrefs.SetInt("totalEnemiesKilled", totalEnemiesKilled);

		int scoreLast;
		if (!PlayerPrefs.HasKey("Highscore"))
			PlayerPrefs.SetInt("Highscore", 0);
		
		scoreLast = PlayerPrefs.GetInt("Highscore");
		
		if (scoreManager.score > scoreLast){
			PlayerPrefs.SetInt("Highscore", scoreManager.score);
			highscoreText.text = "NEW HIGHSCORE!! (" + scoreManager.score + ")";
			
		}
		else{
			highscoreText.text = "Highscore: " + scoreLast;
			
		}

		scoreManager.score = 0;
		gruntsKilled = 0;
		tanksKilled = 0;
		archersKilled = 0;
		
	}
	public void Alive(){

        GameObject.FindGameObjectWithTag("MenuMaster").GetComponent<MenuManager>().ShowMenu(canvasPlayMenu);

        /*
	    canvasEnd.SetActive(false);
	    canvasPlay.SetActive(true);
	    controlSticks.SetActive (true);
        */
	}

	public void Dead(int score){

        GameObject.FindGameObjectWithTag("MenuMaster").GetComponent<MenuManager>().ShowMenu(canvasEndMenu);
        /*
		canvasEnd.SetActive(true);
		canvasPlay.SetActive(false);
		controlSticks.SetActive (false);
       */
		
		scoreText.text = "Score: " + score;
		gruntsText.text = "Grunts Killed: " + gruntsKilled;
		tanksText.text =  "Tanks Killed: " + tanksKilled;
		archersText.text = "Archers Killed: " + archersKilled;
		
		SaveStats();
	}
}
