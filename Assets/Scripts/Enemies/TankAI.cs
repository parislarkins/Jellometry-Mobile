using UnityEngine;
using System.Collections;

public class TankAI : MonoBehaviour 
{
	public float damage = 25f;
	public float knockBackForceJump = 10f;
	public float attackRange = 10;
	public float jumpRange = 10;
	public bool jumpAttack = false;
	public float distance;

	public float jumpTime = 4f;
	private float jumpTimer = 0;
	private bool canJump = true;

	public GameObject meshObject;

	public ParticleSystem walkingParticles;
	public ParticleSystem jumplandParticles;

	private GameObject player;
	private Transform destination;
	private NavMeshAgent agent;

	private Animator anim;

	void Start ()
	{
		player = GameObject.Find("Player");
		destination = player.transform;
		agent = gameObject.GetComponent<NavMeshAgent>();
		
		agent.SetDestination(destination.position);

		anim = meshObject.GetComponent<Animator> ();

		jumpTimer = jumpTime;
	
	}

	void Update () 
	{	
		distance = Vector3.Distance(this.transform.position, player.transform.position);
		
		if (distance <= attackRange && canJump) {
			walkingParticles.Stop ();
			anim.SetBool ("AttackJump", true);
			jumpAttack = true;
			canJump = false;
			//agent.Stop ();
		}

		if (!canJump) {
			jumpTimer -= Time.deltaTime;
		}

		if (jumpTimer <= 0) {
			canJump = true;
			jumpTimer = jumpTime;

		}

		if (!jumpAttack){
			agent.Resume ();
			agent.SetDestination(destination.position);

		}

		agent.SetDestination(destination.position);

	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.tag == "Player") {
			Debug.Log("U got hit by " + this.name + " rekt");
		}
	}

	void Dead(){
		enabled = false;

	}

	void JumpLand(){

		if (distance <= jumpRange) {
			// Knocks back the player
			Vector3 dir = ((player.transform.position - transform.position) * 10).normalized;
			dir.y += knockBackForceJump / 15;
			player.GetComponent<Rigidbody>().velocity = dir * knockBackForceJump / distance * 2;
			player.SendMessage("TakeDamage", damage / distance * 2);                                                                                       

		}

		jumplandParticles.Play ();

		// DECAL FLOOR DO DMG
	}

	void JumpFinished(){
		walkingParticles.Play ();
		anim.SetBool ("AttackJump", false);
		jumpAttack = false;

	}
}