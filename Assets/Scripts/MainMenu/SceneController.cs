using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    // Initialize Canvases
	public GameObject canvasMenu;
	public GameObject canvasHighscores;
	
    // Text Objects
	public GameObject highscoresObj;
	public GameObject totalKillsObj;
	
    // Texts
	private Text highscoresText;
	private Text totalKillsText;

	void LoadGame () {
		Application.LoadLevel(1);
	}
	
	void LoadHighscores(){
		canvasMenu.SetActive(false); // Disable
		canvasHighscores.SetActive(true); // enable
		
        // Set texts
		highscoresText = highscoresObj.GetComponent<Text>();
		totalKillsText = totalKillsObj.GetComponent<Text>();
		
        // Set text
		highscoresText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
		totalKillsText.text = "Total Kills: " + PlayerPrefs.GetInt("totalEnemiesKilled").ToString();
		
	}
	
    // Return to menu
	void BackToMenu(){
		canvasMenu.SetActive(true);
		canvasHighscores.SetActive(false);
	}
	
	void ExitGame(){
		Application.Quit();
	}
}
