import UnityEngine.UI;
var startingHealth : float = 100;          // The amount of health the enemy starts the game with.
var currentHealth : float;                    // The current health the enemy has.
var sinkSpeed : float = 2.5f;            // The speed at which the enemy sinks through the floor when dead.
var scoreValue : int = 10;                 // The amount added to the player's score when the enemy dies.
var deathClip : AudioClip;                 // The sound to play when the enemy dies.

private var enemyAudio : AudioSource;             // Reference to the audio source.
public var hitParticles : ParticleSystem;            // Reference to the particle system that plays when the enemy is damaged.
public var walkingParticles : ParticleSystem;
public var explodeParticles : ParticleSystem;

private var capsuleCollider : CapsuleCollider;    // Reference to the capsule collider.
private var isDead : boolean;                                 // Whether the enemy is dead.
private var sinking : boolean = false;                             // Whether the enemy has started sinking through the floor.
private var Player : GameObject;
private var scoreManager : ScoreManagerJS;
private var healthReduction : float;

private var statsManager : StatsJS;

function Awake ()
{
	Player = GameObject.Find("Player");
	scoreManager = Player.GetComponent(ScoreManagerJS);
    enemyAudio = GetComponent (AudioSource);
    capsuleCollider = GetComponent (CapsuleCollider);
    // Setting the current health when the enemy first spawns.
    currentHealth = startingHealth;
    
    statsManager = GameObject.Find("Statistics").GetComponent.<StatsJS>();
    
//    healthReduction = transform.localScale.x / 2;
//    Debug.Log(healthReduction);
//    healthReduction /= startingHealth / 10;
//	Debug.Log(healthReduction);
//    
}

function Update ()
{    
    // If the current health is less than or equal to zero...
    if (currentHealth <= 0 && !sinking)
    {
		// ... the enemy is dead.
    	Death ();
   		sinking = true;
    	
    }
    
    if (sinking){
    	transform.position.y -= sinkSpeed * Time.deltaTime;
    
    }  
    
}

public function TakeDamage (amount : int, hitPoint : Vector3)
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
    /**
    var parentObject : GameObject = gameObject.parent;
    if (parentObject != null){
	    transform.LookAt(parentObject.transform);
    
    }**/
    // And play the particles.
    hitParticles.Play();
    
    //transform.localScale = new Vector3(transform.localScale.x - healthReduction, transform.localScale.y - healthReduction, transform.localScale.z - healthReduction);
	
}

function Death ()
{
	if (isDead)
		return;
		
	walkingParticles.Stop();
	explodeParticles.Play();

    // The enemy is dead.
    isDead = true;
    
    scoreManager.score += scoreValue;
    
    gameObject.SendMessage("Dead");
    
    gameObject.GetComponent.<NavMeshAgent>().enabled = false;

    // Turn the collider into a trigger so shots can pass through it.
    gameObject.GetComponent.<Collider>().isTrigger = true;

    // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
    enemyAudio.clip = deathClip;
    enemyAudio.Play ();
    Destroy(gameObject,2);
    switch(gameObject.tag){
    	case "tank":
    		Debug.Log(statsManager.tanksKilled);
    		statsManager.tanksKilled ++;
    		Debug.Log(statsManager.tanksKilled);
    		break;
    	case "archer":
    		statsManager.archersKilled ++;
    		break;
    	case "grunt":
    		statsManager.gruntsKilled ++;
    		break;
    }
    
	scoreManager.waveKilled++;
}
