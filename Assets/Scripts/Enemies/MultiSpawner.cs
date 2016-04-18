using UnityEngine;
using System.Collections;
using System;

public class MultiSpawner : MonoBehaviour {

	public GameObject[] enemies;
	//private var spawners : GameObject[];
	public GameObject tank;
	public GameObject grunt;
	public GameObject mortar;
	public GameObject archer;
	public int tanks;
	public int grunts;
	public int mortars;
	public int archers;
	private int maxTanks;
	private int maxGrunts;
	//private int maxMortars;
	private int maxArchers;
	public int wave = 0;
	public int spawnEnemies;
	private int numSpawn;
	
	private UnityEngine.Random rand;
	public int time;
	public int delay;
	public GameObject[] spawners;
	private GameObject Player;
	private ScoreManager scoreManager;
	private Stats statsManager;
    public GameObject healthRefill;

	// Use this for initialization
	void Start () {
		spawners = GameObject.FindGameObjectsWithTag("spawner");
		statsManager = GameObject.Find("Statistics").GetComponent<Stats>();
		Player = GameObject.Find("Player");
		scoreManager = Player.GetComponent<ScoreManager>();
		enemies = new GameObject [4];
		enemies[0] = tank;
		enemies[1] = grunt;
		enemies[2] = mortar;
		enemies[3] = archer;
		SpawnWave();
	}

	void RandomSpawn(){
		if(time >= delay){
			time=0;
			switch(UnityEngine.Random.Range(0,4)){
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
				//if(mortars < maxMortars){
				//	Debug.Log("spawn mortar");
				//	Instantiate(mortar,transform.position,transform.rotation);
				//	mortars++;
				//}
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

    void SpawnHealthRefill(int number) {
        for (int i = 0; i < number;i++)
        {
            int rand = UnityEngine.Random.Range(0, spawners.Length);
            Instantiate(healthRefill, (spawners[rand].gameObject.transform.position),healthRefill.gameObject.transform.rotation);
        }
    }

    void SpawnWave()
    {
		if(wave != 0){
			spawnEnemies = (wave + 1) * 5;
            if(wave > 1){
                SpawnHealthRefill(wave - 1);
            }
		}else{
			spawnEnemies = 5;
		}
		maxGrunts += Convert.ToInt32(Math.Floor(spawnEnemies * 0.7));
//		Debug.Log ("Max Grunts " + maxGrunts);
		maxTanks += Convert.ToInt32(Math.Ceiling(spawnEnemies * 0.15));
//		Debug.Log ("Max Tanks " + maxTanks);
		int waveGrunts = Convert.ToInt32(Math.Floor(spawnEnemies * 0.7));
		int waveTanks = Convert.ToInt32(Math.Ceiling(spawnEnemies * 0.15));
		int waveArchers = spawnEnemies - waveGrunts - waveTanks;
		maxArchers += waveArchers;
//		Debug.Log ("Max Archers " + maxArchers);
		int spawnedGrunts = 0;
		int spawnedTanks = 0;
        int spawnedArchers = 0;

//		Debug.Log ("grunts: " + waveGrunts);
//		Debug.Log ("tanks: " + waveTanks);
//		Debug.Log ("archers: " + waveArchers);

		do{
			//numSpawn = UnityEngine.Random.Range(1,4);
			//for (int j = 1; j <= spawnEnemies; j++){
				int whichEnemy = UnityEngine.Random.Range(0,3);
				int whichSpawner = UnityEngine.Random.Range(0,3);
				switch(whichEnemy){
					case 0:
						if(spawnedGrunts < waveGrunts){
							Instantiate(grunt,spawners[whichSpawner].transform.position,spawners[whichSpawner].transform.rotation);
							grunts++;
							spawnedGrunts++;
							wait (1f);
//							Debug.Log ("spawned grunts: " + spawnedGrunts);
//							Debug.Log ("spawned tanks: " + spawnedTanks);
//							Debug.Log ("spawned archers: " + spawnedArchers);
						}
					break;
					case 1:
						if(spawnedTanks < waveTanks){
							Instantiate(tank,spawners[whichSpawner].transform.position,spawners[whichSpawner].transform.rotation);
							tanks++;
							spawnedTanks++;
							wait (1f);
//							Debug.Log ("spawned grunts: " + spawnedGrunts);
//							Debug.Log ("spawned tanks: " + spawnedTanks);
//							Debug.Log ("spawned archers: " + spawnedArchers);
					}
					break;
					case 2:
						if(spawnedArchers < waveArchers){
							Instantiate(archer,spawners[whichSpawner].transform.position,spawners[whichSpawner].transform.rotation);
							archers++;
							spawnedArchers++;
							wait (1f);
//							Debug.Log ("spawned grunts: " + spawnedGrunts);
//							Debug.Log ("spawned tanks: " + spawnedTanks);
//							Debug.Log ("spawned archers: " + spawnedArchers);
					}
					break;
				}
			//}
			wait (1f);
		}while((spawnedTanks + spawnedGrunts + spawnedArchers) < (waveArchers + waveTanks + waveGrunts));
//		Debug.Log("Finished spawning");
//		Debug.Log ("spawned grunts: " + spawnedGrunts);
//		Debug.Log ("spawned tanks: " + spawnedTanks);
//		Debug.Log ("spawned archers: " + spawnedArchers);
//		Debug.Log ("wave grunts: " + waveGrunts);
//		Debug.Log ("wave tanks: " + waveTanks);
//		Debug.Log ("wave archers: " + waveArchers);
		spawnedTanks=0;
		spawnedGrunts=0;
		spawnedArchers=0;
		waveGrunts = 0;
		waveArchers = 0;
		waveTanks = 0;
		wave++;
	}

	IEnumerator wait(float time){
		yield return new WaitForSeconds (time);
	}

	// Update is called once per frame
	void Update () {
		scoreManager.wave = wave;
		if(GameObject.FindGameObjectsWithTag("tank").Length + GameObject.FindGameObjectsWithTag("archer").Length + GameObject.FindGameObjectsWithTag("grunt").Length == 0 || wave == 0){
			scoreManager.waveKilled = 0;
			SpawnWave();
		}
	}
}
