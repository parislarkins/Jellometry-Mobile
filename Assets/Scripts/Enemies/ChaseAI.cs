using UnityEngine;
using System.Collections;

public class ChaseAI : MonoBehaviour 
{
	public float attackRange = 10;
	public float distance;
	private GameObject player;
	public bool attack  = false;
	private Transform destination;
	private NavMeshAgent agent;

	void Start ()
	{
		player = GameObject.Find("Player");
		destination = player.transform;
		agent = gameObject.GetComponent<NavMeshAgent>();
		
		agent.SetDestination(destination.position);
	}
	void Update () 
	{	

		distance = Vector3.Distance(this.transform.position, player.transform.position);
		
		if(distance <= attackRange)
		{
			attack = true;
			agent.Stop();
		}else{
			attack = false;
			agent.Resume ();
			agent.SetDestination(destination.position);
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.tag == "Player") {
			Debug.Log("U got hit rekt");
		}
	}
}