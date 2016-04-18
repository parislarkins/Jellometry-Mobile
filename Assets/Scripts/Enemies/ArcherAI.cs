using UnityEngine;
using System.Collections;

public class ArcherAI : MonoBehaviour 
{
	public float attackRange = 13f;
	public float distance;
	private GameObject player;
	public bool attack  = false;
	private Transform destination;
	private NavMeshAgent agent;
	public float fireRate = 100f;
	private int time = 90;
	public float speed = 1000;
	//private Transform launcher;
	public GameObject projectile;

	void Start ()
	{
		//launcher = transform.FindChild ("launcher");
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

		if (attack && time >= fireRate) {
			attackPlayer ();
		} else if (attack) {
			time++;
		}
	}
	void Dead(){
		enabled = false;
	}

	void attackPlayer(){
		transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
	//	bullet = Instantiate(projectile,launcher.transform.position, transform.rotation
		//bullet.AddForce(transform.forward * speed);
		time = 0;
	}

	
}