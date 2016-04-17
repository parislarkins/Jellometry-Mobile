#pragma strict

private var totalEnemiesKilled : int;
public var gruntsKilled : int;
public var tanksKilled : int;
public var archersKilled : int;

private var deadCanvas : GameObject;
public var highscoreObj : GameObject;
public var scoreObj : GameObject;
public var gruntsObj : GameObject;
public var tanksObj : GameObject;
public var archersObj : GameObject;

private var highscoreText : Text;
private var scoreText : Text;
private var gruntsText : Text;
private var tanksText : Text;
private var archersText : Text;

private var scoreManager : ScoreManagerJS;

public var canvasEnd : GameObject;
public var canvasPlay : GameObject;

function Start () {

	if (PlayerPrefs.GetInt("totalEnemiesKilled") == null){
		PlayerPrefs.SetInt("totalEnemiesKilled", 0);
	}
	
	totalEnemiesKilled = PlayerPrefs.GetInt("totalEnemiesKilled");
	
	scoreManager = GameObject.Find("Player").GetComponent(ScoreManagerJS);
	
	highscoreText = highscoreObj.GetComponent.<Text>();
	scoreText = scoreObj.GetComponent.<Text>();
	gruntsText = gruntsObj.GetComponent.<Text>();
	tanksText = tanksObj.GetComponent.<Text>();
	archersText = archersObj.GetComponent.<Text>();
}

public function SaveStats(){
	totalEnemiesKilled += gruntsKilled + tanksKilled + archersKilled;
	PlayerPrefs.SetInt("totalEnemiesKilled", totalEnemiesKilled);
	Debug.Log(totalEnemiesKilled);
	
	var scoreLast : int;
	if (PlayerPrefs.GetInt("Highscore") == null){
		PlayerPrefs.SetInt("Highscore", 0);
	}
	
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

public function Alive(){
	canvasEnd.SetActive(false);
	canvasPlay.SetActive(true);

}

public function Dead(score : int){
	canvasEnd.SetActive(true);
	canvasPlay.SetActive(false);
	
	scoreText.text = "Score: " + score;
	gruntsText.text = "Grunts Killed: " + gruntsKilled;
	tanksText.text =  "Tanks Killed: " + tanksKilled;
	archersText.text = "Archers Killed: " + archersKilled;

	SaveStats();
	
}