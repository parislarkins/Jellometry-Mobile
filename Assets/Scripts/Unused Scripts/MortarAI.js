#pragma strict

var mortar : GameObject;
var mortarShell : GameObject;

private var attack : boolean = false;
var speed : float = 1000;
var time : int = 100;
private var destination: Transform;
private var agent : NavMeshAgent;
var attackRange : float;
var distance : float;
private var player : GameObject;


function Start () {
	player = GameObject.Find("Player");
	destination = player.transform;
	agent = gameObject.GetComponent.<NavMeshAgent>();
	agent.SetDestination(destination.position);
}

function FixedUpdate () {
	transform.LookAt(new Vector3(GameObject.FindGameObjectWithTag("Player").GetComponent.<Transform>().position.x, 1f, GameObject.FindGameObjectWithTag("Player").GetComponent.<Transform>().position.z));

	distance = Vector3.Distance(this.transform.position, player.transform.position);
	
	if(distance <= attackRange)
	{
		attack = true;
		agent.Stop();
	}else{
		attack = false;
		time = 100;
		agent.Resume();
		agent.SetDestination(destination.position);

	}
	
	if(time >= 100 && attack)
	{		
		transform.LookAt(GameObject.FindGameObjectWithTag("Player").GetComponent.<Transform>());
		var bullet : Rigidbody = Instantiate(mortarShell,mortar.transform.position, transform.rotation).GetComponent.<Rigidbody>();
		bullet.SendMessage("Shot", gameObject);
		bullet.AddForce(transform.forward * speed);
		time = 0;
	}else if (attack){
		time++;
	}
	agent.SetDestination(destination.position);
	
}	
function OnCollisionEnter(collision : Collision)
{
	if (collision.collider.gameObject.tag == "Player") {
		Debug.Log("U got hit by " + this.name + " rekt");
	}
}

function Dead(){
	enabled = false;

}