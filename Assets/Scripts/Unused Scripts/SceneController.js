#pragma strict
import UnityEngine.Event;
import UnityEngine.UI;

public var canvasMenu : GameObject;
public var canvasHighscores : GameObject;

public var highscoresObj : GameObject;
public var totalKillsObj : GameObject;

private var highscoresText : Text;
private var totalKillsText : Text;


function LoadGame(){
	Application.LoadLevel(1);
	
}

function LoadHighscores(){
	canvasMenu.SetActive(false);
	canvasHighscores.SetActive(true);
	
	highscoresText = highscoresObj.GetComponent.<Text>();
	totalKillsText = totalKillsObj.GetComponent.<Text>();
	
	highscoresText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
	totalKillsText.text = "Total Kills: " + PlayerPrefs.GetInt("totalEnemiesKilled").ToString();
		
}

function BackToMenu(){
	canvasMenu.SetActive(true);
	canvasHighscores.SetActive(false);

}

function ExitGame(){
	Application.Quit();

}