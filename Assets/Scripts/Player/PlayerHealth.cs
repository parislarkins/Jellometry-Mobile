using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public Slider healthSlider;
	public float maxHealth = 100f;
	public float currentHealth= 0f;
	private GameObject Player;
	private ScoreManager scoreManager;
	
	private bool dead = false;
	public GameObject tempSats;
	private Stats statsManager;
	
	public float timer = 1f;
	private bool canExit = false;
	// Use this for initialization

	void Start () {
		Player = GameObject.Find("Player");
		scoreManager = Player.GetComponent<ScoreManager>();
		currentHealth = maxHealth;
		healthSlider.maxValue = maxHealth;
		healthSlider.value = maxHealth;
		
		statsManager = GameObject.Find("Statistics").GetComponent<Stats>();
	}
	
	// Update is called once per frame
	void Update () {

        // Set to dead if no health
		if (currentHealth <= 0 && !dead){			
			statsManager.Dead(scoreManager.score);
			dead = true;
			
		}
		else{
			healthSlider.value = currentHealth;
		}
		
		if (dead){
			timer -= Time.deltaTime;
			
		}
		
		if (timer <= 0){
			canExit = true;
			
		}
		
        // check if fallen off level
		if(gameObject.transform.position.y < -14){
			TakeDamage(currentHealth);
		}
		
		if (Input.GetKey(KeyCode.Escape)){
			Application.LoadLevel(0);
			
		}
		
		if (dead && Input.anyKey && !Input.GetKey(KeyCode.Escape) && canExit){
			LoadLevel();
			
		}
		scoreManager.health = Convert.ToInt32 (currentHealth);
	}

    // Load levels
	void LoadLevel(){
		if (canExit) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}

    // Take damage from other sources
	void TakeDamage(float damageTaken){
		currentHealth -= damageTaken;
		scoreManager.damageTaken += Convert.ToInt32 (damageTaken);
		
	}
	
    // Hit by archer bullet
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Archerbullet"){
			TakeDamage(20);
			Destroy(collision.gameObject);
		}
	}
}
