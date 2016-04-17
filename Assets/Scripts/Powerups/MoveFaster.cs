using UnityEngine;
using System.Collections;

public class MoveFaster : MonoBehaviour {

	public float increaseAmount = 3f;
	private GameObject Player;
	private PlayerControls playerControls;
	
	// Use this for initialization
	void Start(){
		Player = GameObject.Find("Player");
		playerControls = Player.GetComponent<PlayerControls>();
	}
	void OnTriggerEnter(Collider thing){
		if (thing.gameObject.tag == "Player"){
			playerControls.speed += increaseAmount;
			playerControls.moveFasterStart = Time.time;
			Destroy(gameObject);
		}
	}
}
