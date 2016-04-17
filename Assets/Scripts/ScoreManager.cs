using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int score = 0;        // The player's score.
	public int health;
	public int damageTaken;
	int remaining;
	public int waveKilled;
	public int wave = 1;
	
	private DateTime startTime;
	public Text healthText;      // Reference to the Text component.
	public Text scoreText;
	public Text waveText;
	public Text remainingText;

	// Use this for initialization
	void Awake () {
		// Set up the reference.
		startTime = DateTime.Now;
		//healthText = GetComponentInChildren.<Text>();
		//scoreText = GetComponentInChildren.<Text>();
		// Reset the score.
		score = 0;
		health = 100;
		wave = 1;
	}
	
	// Update is called once per frame
	void Update () {
		remaining = wave * 5 - waveKilled;
		// Set the displayed text to be the word "Score" followed by the score value.
		scoreText.text = "" + score;
		healthText.text = health.ToString();
		waveText.text = "WAVE:"+wave;
		remainingText.text = "REMAINING: " + remaining.ToString();
	}
}
