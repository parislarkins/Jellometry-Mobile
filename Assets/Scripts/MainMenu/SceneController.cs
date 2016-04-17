using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

	public GameObject canvasMenu;
	public GameObject canvasHighscores;
	
	public GameObject highscoresObj;
	public GameObject totalKillsObj;
	
	private Text highscoresText;
	private Text totalKillsText;

	// Use this for initialization
	void LoadGame () {
		Application.LoadLevel(1);
	}
	
	void LoadHighscores(){
		canvasMenu.SetActive(false);
		canvasHighscores.SetActive(true);
		
		highscoresText = highscoresObj.GetComponent<Text>();
		totalKillsText = totalKillsObj.GetComponent<Text>();
		
		highscoresText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
		totalKillsText.text = "Total Kills: " + PlayerPrefs.GetInt("totalEnemiesKilled").ToString();
		
	}
	
	void BackToMenu(){
		canvasMenu.SetActive(true);
		canvasHighscores.SetActive(false);
	}
	
	void ExitGame(){
		Application.Quit();
	}
}
