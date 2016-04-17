using UnityEngine;
using System.Collections;

public class SingleSpawner : MonoBehaviour {

	public GameObject enemy;
	public int maxEnemies = 4;
	public int numEnemies;
	
	Random rand;
	public int time;
	int delay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(time >= delay)
		{
			if(numEnemies < maxEnemies)
			{
				Instantiate(enemy,transform.position,transform.rotation);
			}
			numEnemies ++;
			time = 0;
		}
		time++;
	}
}
