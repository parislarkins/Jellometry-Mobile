using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	int damagePerShot = 20;                  // The damage inflicted by each bullet.
	float timeBetweenBullets = 0.23f;        // The time between each shot.
	float range = 100f;                      // The distance the gun can fire.
	
	private float timer;                            // A timer to determine when to fire.
	private Ray shootRay;                         // A ray from the gun end forwards.
	private RaycastHit shootHit;                   // A raycast hit to get information about what was hit.
	private int shootableMask;                    // A layer mask so the raycast only hits things on the shootable layer.
	private ParticleSystem gunParticles;            // Reference to the particle system.
	private AudioSource gunAudio;                   // Reference to the audio source.
	private Light gunLight;                         // Reference to the light component.
	private float effectsDisplayTime = 0.2f;         // The proportion of the timeBetweenBullets that the effects will display for.
	
	public GameObject hitParticles;
	public ParticleSystem bulletParticles;

	// Use this for initialization
	void Start () {
		// Create a layer mask for the Shootable layer.
		shootableMask = LayerMask.GetMask ("Shootable");
		
		// Set up the references.
		gunParticles = GetComponent<ParticleSystem>();
		gunAudio = GetComponent<AudioSource>();
		gunLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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

	void DisableEffects (){
		// Disable the light.
		gunLight.enabled = false;
	}

	void Shoot (){
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
		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			if (shootHit.collider.gameObject.tag == "obstacle") {
				//    		var particles : ParticleSystem = Instantiate(hitParticles,shootHit.point,shootHit.transform.rotation).GetComponent.<ParticleSystem>();
				//   			var hitColor : Color = shootHit.collider.GetComponent.<Renderer>().material.GetColor("_Color");
				//   			particles.GetComponent.<Renderer>().material.SetColor("_TintColor",hitColor);
				
				GameObject partSysObj;
				partSysObj = Instantiate(hitParticles, shootHit.point, Quaternion.identity) as GameObject;
				ParticleSystem partSys = partSysObj.GetComponent<ParticleSystem>();
				Debug.Log(partSys);
				Color hitColor = shootHit.collider.GetComponent<Renderer>().material.GetColor("_Color");
				partSys.GetComponent<Renderer>().material.SetColor("_TintColor", hitColor);
				
				Vector3 dirVector = new Vector3(shootHit.collider.gameObject.transform.position.x, partSys.transform.position.y, shootHit.collider.gameObject.transform.position.z);
				partSysObj.transform.LookAt(dirVector);
				//partSysObj.transform.rotation.eulerAngles.y += 180;
				partSysObj.transform.RotateAround(shootHit.point,Vector3.up,180f);
				
				float distance = Vector3.Distance(transform.position, shootHit.point);
				float time =  distance / bulletParticles.startSpeed;
				
				wait (time);
				partSys.Play();
				
			}
			
			//Try and find an EnemyHealth script on the gameobject hit.
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
			
			// If the EnemyHealth component exist...
			if(enemyHealth != null)
			{   
				// ... the enemy should take damage.
				enemyHealth.TakeDamage (damagePerShot, shootHit.point);
			}
		}
	}

	IEnumerator wait(float time){
		yield return new WaitForSeconds (time); 
	}
}
