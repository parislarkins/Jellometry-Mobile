using UnityEngine;
using System.Collections;

public class ShootFaster : MonoBehaviour {

	public float increaseAmount = .05f;
	private GameObject Player;
	private PlayerControls playerControls;
	
	// Use this for initialization
	void Start(){
		Player = GameObject.Find("Player");
		playerControls = Player.GetComponent<PlayerControls>();
	}
	void OnTriggerEnter(Collider thing){
		if (thing.gameObject.tag == "Player"){
			playerControls.timeBetweenBullets -= increaseAmount;
			playerControls.shootFasterStart = Time.time;
			Destroy(gameObject);
		}
	}
}
