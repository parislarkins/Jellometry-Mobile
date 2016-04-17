#pragma strict
import System;

private var enemies : GameObject[];
public var spawners : GameObject[];
public var tank : GameObject;
public var grunt : GameObject;
public var mortar : GameObject;
public var archer : GameObject;
public var tanks : int;
public var grunts : int;
public var mortars : int;
public var archers : int;
private var maxTanks : int;
private var maxGrunts : int;
private var maxMortars : int;
private var maxArchers : int;
public var wave : int = 0;
var spawnEnemies : int;
var numSpawn : int;

var rand : UnityEngine.Random;
public var time : int;
public var delay : int;

private var Player : GameObject;
//private var scoreManager : ScoreManagerJS;
//private var statsManager : StatsJS;

function Start () {
	spawners = GameObject.FindGameObjectsWithTag("spawner");
	//statsManager = GameObject.Find("Statistics").GetComponent(StatsJS);
	Player = GameObject.Find("Player");
	//scoreManager = Player.GetComponent(ScoreManagerJS);
	enemies[0] = tank;
	enemies[1] = grunt;
	enemies[2] = mortar;
	enemies[3] = archer;
	SpawnWave();
}

function SpawnWave(){
	if(wave > 1){
		spawnEnemies = (wave - 1) * 5;
	}else{
		spawnEnemies = (wave) * 5;
	}
	maxGrunts += Convert.ToInt32(spawnEnemies * 0.7);
	maxTanks += Convert.ToInt32(spawnEnemies * 0.15);
	var waveGrunts : int = Convert.ToInt32(Math.Floor(spawnEnemies * 0.7));
	var waveTanks : int = Convert.ToInt32(Math.Ceiling(spawnEnemies * 0.15));
	var waveArchers : int = spawnEnemies - waveGrunts - waveTanks;
	maxArchers += waveArchers;
	var spawnedGrunts : int = 0;
	var spawnedTanks : int = 0;
	var spawnedArchers : int = 0;
	Debug.Log("Max Grunts " + maxGrunts);
	Debug.Log("Max tanks " + maxTanks);
	Debug.Log("Max archers " + maxArchers);
	Debug.Log ("grunts: " + waveGrunts);
	Debug.Log ("tanks: " + waveTanks);
	Debug.Log ("archers: " + waveArchers);
	
	do
	{
		var whichEnemy : int = UnityEngine.Random.Range(0,3);
		var whichSpawner : int = UnityEngine.Random.Range(0,3);
		switch(whichEnemy){
			case 0:
				if(spawnedGrunts < waveGrunts){
					Instantiate(grunt,spawners[whichSpawner].transform.position,spawners[1].transform.rotation);
					grunts++;
					spawnedGrunts++;
					yield WaitForSeconds(0.2);
					Debug.Log ("spawned grunts: " + spawnedGrunts);
					Debug.Log ("spawned tanks: " + spawnedTanks);
					Debug.Log ("spawned archers: " + spawnedArchers);
				}
			break;
			case 1:
				if(spawnedTanks < waveTanks){
					Instantiate(tank,spawners[whichSpawner].transform.position,spawners[1].transform.rotation);
					tanks++;
					spawnedTanks++;
					yield WaitForSeconds(0.2);
					Debug.Log ("spawned grunts: " + spawnedGrunts);
					Debug.Log ("spawned tanks: " + spawnedTanks);
					Debug.Log ("spawned archers: " + spawnedArchers);
				}
			break;
			case 2:
				if(spawnedArchers < waveArchers){
					Instantiate(archer,spawners[whichSpawner].transform.position,spawners[1].transform.rotation);
					archers++;
					spawnedArchers++;
					yield WaitForSeconds(0.2);
					Debug.Log ("spawned grunts: " + spawnedGrunts);
					Debug.Log ("spawned tanks: " + spawnedTanks);
					Debug.Log ("spawned archers: " + spawnedArchers);
				}
			break;
		}
		yield WaitForSeconds(0.7);
	}while((spawnedTanks + spawnedGrunts + spawnedArchers) < (waveArchers + waveTanks + waveGrunts));
	Debug.Log("Finished spawning");
	Debug.Log ("spawned grunts: " + spawnedGrunts);
	Debug.Log ("spawned tanks: " + spawnedTanks);
	Debug.Log ("spawned archers: " + spawnedArchers);
	Debug.Log ("wave grunts: " + waveGrunts);
	Debug.Log ("wave tanks: " + waveTanks);
	Debug.Log ("wave archers: " + waveArchers);
	spawnedTanks=0;
	spawnedGrunts=0;
	spawnedArchers=0;
	waveGrunts = 0;
	waveArchers = 0;
	waveTanks = 0;
	wave++;
}

function Update(){
	//scoreManager.wave = wave;
	if(GameObject.FindGameObjectsWithTag("tank").Length + GameObject.FindGameObjectsWithTag("archer").Length + GameObject.FindGameObjectsWithTag("grunt").Length == 0){
		wave++;
		//scoreManager.waveKilled = 0;
		SpawnWave();
	}
}