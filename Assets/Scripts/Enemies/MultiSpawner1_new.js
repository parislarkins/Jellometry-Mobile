#pragma strict
import System;

private var enemies : GameObject[];
//private var spawners : GameObject[];
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
private var scoreManager : ScoreManagerJS;
private var statsManager : StatsJS;

function Start () {
	statsManager = GameObject.Find("Statistics").GetComponent(StatsJS);

	Player = GameObject.Find("Player");
	scoreManager = Player.GetComponent(ScoreManagerJS);
	enemies[0] = tank;
	enemies[1] = grunt;
	enemies[2] = mortar;
	enemies[3] = archer;
	SpawnWave();
}

function RandomSpawn(){
	if(time >= delay){
		time=0;
		var r : int = rand.Range(0,4);
		switch(r){
		case 0:
			if(tanks < maxTanks){
				Debug.Log("spawn tank");
				Instantiate(tank,transform.position,transform.rotation);
				tanks++;	
			}
			break;
		case 1:
			if(grunts < maxGrunts){
				Debug.Log("spawn grunt");
				Instantiate(grunt,transform.position,transform.rotation);
				grunts++;
			}
			break;
		case 2:
			if(mortars < maxMortars){
				Debug.Log("spawn mortar");
				Instantiate(mortar,transform.position,transform.rotation);
				mortars++;
			}
			break;
		case 3:
			if(archers < maxArchers){
				Debug.Log("spawn archer");
				Instantiate(archer,transform.position,transform.rotation);
				archers++;
			}
			break;
		}
	}
	time++;
}

function SpawnWave(){
	if(wave != 0){
		spawnEnemies = wave * 5;
	}else{
		spawnEnemies = 5;
	}
	maxGrunts += Convert.ToInt32(spawnEnemies * 0.7);
	maxTanks += Convert.ToInt32(spawnEnemies * 0.15);
	maxArchers += spawnEnemies - maxGrunts - maxTanks;
	var waveGrunts : int = Convert.ToInt32(spawnEnemies * 0.7);
	var waveTanks : int = Convert.ToInt32(spawnEnemies * 0.15);
	var waveArchers : int = spawnEnemies - waveGrunts - waveTanks;
	var spawnedGrunts : int = 0;
	var spawnedTanks : int = 0;
	var spawnedArchers : int = 0;
	var spawners : GameObject[] = GameObject.FindGameObjectsWithTag("obstacle");
	
	do{
		numSpawn = UnityEngine.Random.Range(1,4);
		for (var j : int = 1; j <= numSpawn; j++){
			var whichEnemy : int = UnityEngine.Random.Range(0,3);
			var whichSpawner : int = UnityEngine.Random.Range(0,3);
			switch(whichEnemy){
				case 0:
					if(spawnedGrunts <= waveGrunts){
						Instantiate(grunt,spawners[whichSpawner].transform.position,spawners[1].transform.rotation);
						grunts++;
						spawnedGrunts++;
						yield WaitForSeconds(0.2);
					}
				break;
				case 1:
					if(spawnedTanks <= waveTanks){
						Instantiate(tank,spawners[whichSpawner].transform.position,spawners[1].transform.rotation);
						tanks++;
						spawnedTanks++;
						yield WaitForSeconds(0.2);
					}
				break;
				case 2:
					if(spawnedArchers <= waveArchers){
						Instantiate(archer,spawners[whichSpawner].transform.position,spawners[1].transform.rotation);
						archers++;
						spawnedArchers++;
						yield WaitForSeconds(0.2);
					}
				break;
			}
		}
		yield WaitForSeconds(0.7);
	}while(spawnEnemies > spawnedTanks + spawnedGrunts + spawnedArchers);
	spawnedTanks=0;
	spawnedGrunts=0;
	spawnedArchers=0;
}

function Update(){
	scoreManager.wave = wave;
	if(GameObject.FindGameObjectsWithTag("tank").Length + GameObject.FindGameObjectsWithTag("archer").Length + GameObject.FindGameObjectsWithTag("grunt").Length == 0){
		wave++;
		scoreManager.waveKilled = 0;
		SpawnWave();
	}
}