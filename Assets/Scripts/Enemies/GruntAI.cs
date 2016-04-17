using UnityEngine;
using System.Collections;

public class GruntAI : MonoBehaviour 
{
	public float damage = 10;
	public float knockBackForce = 10f;
	public float attackRange = 5;
	public float distance;

	private GameObject player;
	private Transform destination;
	private NavMeshAgent agent;

	void Start ()
	{
		player = GameObject.Find("Player");
		destination = player.transform;
		agent = gameObject.GetComponent<NavMeshAgent>();

	}
	void Update () 
	{	
		agent.SetDestination(destination.position);
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.tag == "Player") {
			collision.gameObject.SendMessage("TakeDamage", damage);

			// Knocks back the player
			Vector3 dir = (collision.transform.position - transform.position).normalized;
			dir.y += knockBackForce / 25;
			collision.rigidbody.velocity = dir * knockBackForce;

			Debug.Log("U got hit by " + this.name + " rekt");
		}
	}
	void Dead(){
		enabled = false;

	}
}