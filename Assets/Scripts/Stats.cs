using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    // Enemies Killed
	private int totalEnemiesKilled;
	public int gruntsKilled;
	public int tanksKilled;
	public int archersKilled;
	
    // Canvas objects
	private GameObject deadCanvas;
	public GameObject highscoreObj;
	public GameObject scoreObj;
	public GameObject gruntsObj;
	public GameObject tanksObj;
	public GameObject archersObj;
	
    // Texts used for displayed stats
	private Text highscoreText;
	private Text scoreText;
	private Text gruntsText;
	private Text tanksText;
	private Text archersText;
	
	private ScoreManager scoreManager;
	
    // Menus
    public Menu canvasPlayMenu;
    public Menu canvasEndMenu;

    // Canvases
    public GameObject canvasPlay;
	public GameObject canvasEnd;
	public GameObject controlSticks;

	// Use this for initialization
	void Start () {
        // Set key if doesn't exist
		if (!PlayerPrefs.HasKey ("totalEnemiesKilled"))
			PlayerPrefs.SetInt("totalEnemiesKilled", 0);

        // Set variable
		totalEnemiesKilled = PlayerPrefs.GetInt("totalEnemiesKilled");
	
        // Find Score Manager Script
		scoreManager = GameObject.Find("Player").GetComponent<ScoreManager>();
		
        // Set Text Objects
		highscoreText = highscoreObj.GetComponent<Text>();
		scoreText = scoreObj.GetComponent<Text>();
		gruntsText = gruntsObj.GetComponent<Text>();
		tanksText = tanksObj.GetComponent<Text>();
		archersText = archersObj.GetComponent<Text>();
	}
	
    // Save current stats to player prefs for later use
	void SaveStats(){
		totalEnemiesKilled += gruntsKilled + tanksKilled + archersKilled;
		PlayerPrefs.SetInt("totalEnemiesKilled", totalEnemiesKilled);

        // Compare sessions scores against highscores
		int scoreLast;
		if (!PlayerPrefs.HasKey("Highscore"))
			PlayerPrefs.SetInt("Highscore", 0);
		
		scoreLast = PlayerPrefs.GetInt("Highscore");
		
        // Check which score is larger
		if (scoreManager.score > scoreLast){
            // New highscore
			PlayerPrefs.SetInt("Highscore", scoreManager.score);
			highscoreText.text = "NEW HIGHSCORE!! (" + scoreManager.score + ")";
			
		}
		else{
            // Nothing new 
			highscoreText.text = "Highscore: " + scoreLast;
			
		}

        // Reset
		scoreManager.score = 0;
		gruntsKilled = 0;
		tanksKilled = 0;
		archersKilled = 0;
		
	}
	public void Alive(){
        // Show playing canvas
        GameObject.FindGameObjectWithTag("MenuMaster").GetComponent<MenuManager>().ShowMenu(canvasPlayMenu);
	    controlSticks.SetActive (true); // Show control sticks
	}

	public void Dead(int score){
        // Show end game canvas
        GameObject.FindGameObjectWithTag("MenuMaster").GetComponent<MenuManager>().ShowMenu(canvasEndMenu);
        // Hide control sticks
		controlSticks.SetActive (false);
		
        // Set texts to values
		scoreText.text = "Score: " + score;
		gruntsText.text = "Grunts Killed: " + gruntsKilled;
		tanksText.text =  "Tanks Killed: " + tanksKilled;
		archersText.text = "Archers Killed: " + archersKilled;
		
		SaveStats();
	}
}
