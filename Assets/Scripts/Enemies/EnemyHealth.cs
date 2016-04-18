using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public float startingHealth = 100;          // The amount of health the enemy starts the game with.
	public float currentHealth;                 // The current health the enemy has.
	public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
	AudioClip deathClip;                        // The sound to play when the enemy dies.
	
	private AudioSource enemyAudio;             // Reference to the audio source.
	public ParticleSystem hitParticles;         // Reference to the particle system that plays when the enemy is damaged.
	public ParticleSystem walkingParticles;     // Part system, walking
	public ParticleSystem explodeParticles;     // Part system exploding
	
	private CapsuleCollider capsuleCollider;    // Reference to the capsule collider.
	private bool isDead;                        // Whether the enemy is dead.
	private bool sinking = false;               // Whether the enemy has started sinking through the floor.
	private GameObject Player;                  // Player reference
	private ScoreManager scoreManager;
	
	private Stats statsManager;

	// Use this for initialization
	void Awake () {
		Player = GameObject.Find("Player");
		scoreManager = Player.GetComponent<ScoreManager>();
		enemyAudio = GetComponent<AudioSource>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
		
		statsManager = GameObject.Find("Statistics").GetComponent<Stats>();
		   
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (currentHealth <= 0 && !sinking)
		{
			// ... the enemy is dead.
			Death ();
			sinking = true;
		}
		
		if (sinking){
			//transform.position.y -= sinkSpeed * Time.deltaTime;
		}  

	}

	public void TakeDamage (int amount,Vector3 hitPoint)
	{
		// If the enemy is dead...
		if(isDead)
			// ... no need to take damage so exit the function.
			return;
		
		// Play the hurt sound effect.
		enemyAudio.Play ();
		
		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;
		
		// Set the position of the particle system to where the hit was sustained.
		hitParticles.transform.position = hitPoint;

		// And play the particles.
		hitParticles.Play();
		
	}

	void Death ()
	{
		if (isDead)
			return;
		
        // Stop walking, death explode particles
		walkingParticles.Stop();
		explodeParticles.Play();
		
		// The enemy is dead.
		isDead = true;
		
		scoreManager.score += scoreValue;
		gameObject.SendMessage("Dead");
		
		gameObject.GetComponent<NavMeshAgent>().enabled = false;
		
		// Turn the collider into a trigger so shots can pass through it.
		gameObject.GetComponent<Collider>().isTrigger = true;
		
		// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
		enemyAudio.clip = deathClip;
		enemyAudio.Play ();
		Destroy(gameObject,2);

        // Increase stats
		switch(gameObject.tag){
		    case "tank":
			    statsManager.tanksKilled ++;
			    break;
		    case "archer":
			    statsManager.archersKilled ++;
			    break;
		    case "grunt":
			    statsManager.gruntsKilled ++;
			    break;
		}
		
        // Increase wave
		scoreManager.waveKilled++;
	}
}
