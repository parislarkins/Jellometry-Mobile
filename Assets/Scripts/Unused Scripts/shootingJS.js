#pragma strict

var damagePerShot : int = 20;                  // The damage inflicted by each bullet.
var timeBetweenBullets : float = 0.15f;        // The time between each shot.
var range : float = 100f;                      // The distance the gun can fire.

private var timer : float;                             // A timer to determine when to fire.
private var shootRay : Ray;                            // A ray from the gun end forwards.
private var shootHit : RaycastHit;                     // A raycast hit to get information about what was hit.
private var shootableMask : int;                       // A layer mask so the raycast only hits things on the shootable layer.
private var gunParticles : ParticleSystem;             // Reference to the particle system.
private var gunAudio : AudioSource;                    // Reference to the audio source.
private var gunLight : Light;                          // Reference to the light component.
private var effectsDisplayTime : float = 0.2f;         // The proportion of the timeBetweenBullets that the effects will display for.

public var hitParticles : GameObject;
public var bulletParticles : ParticleSystem;

function Awake ()
{
    // Create a layer mask for the Shootable layer.
    shootableMask = LayerMask.GetMask ("Shootable");

     // Set up the references.
    gunParticles = GetComponent (ParticleSystem);
    gunAudio = GetComponent (AudioSource);
    gunLight = GetComponent (Light);
}


function Update ()
{
    // Add the time since Update was last called to the timer.
    timer += Time.deltaTime;

    // If the Fire1 button is being press and it's time to fire...
    if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets)
    {
        // ... shoot the gun.
        Shoot ();
    }

    // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
    if(timer >= timeBetweenBullets * effectsDisplayTime)
    {
        // ... disable the effects.
        DisableEffects ();
    }
}


public function DisableEffects ()
{
    // Disable the light.
    gunLight.enabled = false;
}


public function Shoot ()
{
    // Reset the timer.
    timer = 0f;

    // Play the gun shot audioclip.
    gunAudio.Play ();

    // Enable the light.
    gunLight.enabled = true;

    // Stop the particles from playing if they were, then start the particles.
    gunParticles.Stop ();
    gunParticles.Play ();

    // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
    shootRay.origin = transform.position;
    shootRay.direction = transform.forward;

    // Perform the raycast against gameobjects on the shootable layer and if it hits something...
    if(Physics.Raycast (shootRay, shootHit, range, shootableMask))
    {
    	if (shootHit.collider.gameObject.tag == "obstacle") {
//    		var particles : ParticleSystem = Instantiate(hitParticles,shootHit.point,shootHit.transform.rotation).GetComponent.<ParticleSystem>();
//   			var hitColor : Color = shootHit.collider.GetComponent.<Renderer>().material.GetColor("_Color");
//   			particles.GetComponent.<Renderer>().material.SetColor("_TintColor",hitColor);
    	
    		var partSysObj : GameObject = Instantiate(hitParticles, shootHit.point, Quaternion.identity);
    		var partSys : ParticleSystem = partSysObj.GetComponent.<ParticleSystem>();
    		Debug.Log(partSys);
    		var hitColor : Color = shootHit.collider.GetComponent.<Renderer>().material.GetColor("_Color");
    		partSys.GetComponent.<Renderer>().material.SetColor("_TintColor", hitColor);
    		
    		var dirVector : Vector3 = new Vector3(shootHit.collider.gameObject.transform.position.x, partSys.transform.position.y, shootHit.collider.gameObject.transform.position.z);
    		partSysObj.transform.LookAt(dirVector);
    		partSysObj.transform.rotation.eulerAngles.y += 180;
    		
    		var distance = Vector3.Distance(transform.position, shootHit.point);
    		var time : float =  distance / bulletParticles.startSpeed;
    		
    		yield WaitForSeconds(time);
    		partSys.Play();
   			
    	}
    	
         //Try and find an EnemyHealth script on the gameobject hit.
        var enemyHealth : EnemyHealthJS = shootHit.collider.GetComponent (EnemyHealthJS);

        // If the EnemyHealth component exist...
        if(enemyHealth != null)
        {   
        // ... the enemy should take damage.
            enemyHealth.TakeDamage (damagePerShot, shootHit.point);
        }
    }
}