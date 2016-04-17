using UnityEngine;
using System.Collections;

public class HealthRefill : MonoBehaviour {

	public int refillAmount = 30;
	private GameObject Player;
	private PlayerHealth playerHealth;

	// Use this for initialization
	void Start(){
		Player = GameObject.Find("Player");
		playerHealth = Player.GetComponent<PlayerHealth>();
	}
	void OnTriggerEnter(Collider thing){
		if (thing.gameObject.tag == "Player" && playerHealth.currentHealth != playerHealth.maxHealth){
			if(playerHealth.currentHealth + refillAmount/2 > playerHealth.maxHealth){
				playerHealth.currentHealth = playerHealth.maxHealth;
			}else{
				playerHealth.currentHealth += refillAmount/2;
				Destroy(gameObject);
			}
		}
	}
}
