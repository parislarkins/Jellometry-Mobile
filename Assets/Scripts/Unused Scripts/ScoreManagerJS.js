#pragma strict
import System;

static var score : int = 0;        // The player's score.
static var health : int;
var damageTaken : int;
var remaining : int;
var waveKilled : int;
static var wave : int = 1;

private var startTime : DateTime;
public var healthText : Text;       // Reference to the Text component.
public var scoreText : Text;
public var waveText : Text;
public var remainingText : Text;

private var statsManager : StatsJS;

function Awake ()
{
    // Set up the reference.
	startTime = DateTime.Now;
	//healthText = GetComponentInChildren.<Text>();
	//scoreText = GetComponentInChildren.<Text>();
    // Reset the score.
    score = 0;
    health = 100;
    wave = 1;
	
	statsManager = GameObject.Find("Statistics").GetComponent(StatsJS);


}

function Update ()
{
	remaining = wave * 5 - waveKilled;
    // Set the displayed text to be the word "Score" followed by the score value.
    scoreText.text = "SCORE:" + score;
    healthText.text = health.ToString();
    waveText.text = "WAVE:"+wave;
    remainingText.text = "REMAINING : " + remaining.ToString();
}
