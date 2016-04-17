#pragma strict
import UnityEngine.UI;
import System;

public var healthSlider : Slider;
public var maxHealth : float = 100f;
public var currentHealth : float = 0f;
private var Player : GameObject;
private var scoreManager : ScoreManagerJS;

private var dead : Boolean = false;
public var tempSats : GameObject;
private var statsManager : StatsJS;

public var timer : float = 1f;
private var canExit : boolean = false;

function Start () {
	Player = GameObject.Find("Player");
	scoreManager = Player.GetComponent(ScoreManagerJS);
	currentHealth = maxHealth;
	healthSlider.maxValue = maxHealth;
	healthSlider.value = maxHealth;
	
	statsManager = GameObject.Find("Statistics").GetComponent(StatsJS);
	
}

function Update () {
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
	
	if(gameObject.transform.position.y < -14){
		TakeDamage(currentHealth);
	}
	
	if (Input.GetKey(KeyCode.Escape)){
		Application.LoadLevel(0);
	
	}
	
	if (dead && Input.anyKey && !Input.GetKey(KeyCode.Escape) && canExit){
		LoadLevel();
		
	}
	
	scoreManager.health = currentHealth;
}

function LoadLevel(){
	if (canExit){
		Application.LoadLevel(Application.loadedLevel);
	}
}

function TakeDamage(damageTaken : float){
	currentHealth -= damageTaken;
	scoreManager.damageTaken += damageTaken;
	
}

function OnCollisionEnter(collision : Collision){
	if (collision.gameObject.tag == "Archerbullet"){
		TakeDamage(20);
		Destroy(collision.gameObject);
	}
}