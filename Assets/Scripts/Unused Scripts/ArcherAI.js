

public var attackRange : float = 13f;
public var distance : float;
private var player : GameObject ;
public var attack : boolean = false;
private var destination: Transform;
private var agent : NavMeshAgent;
public var fireRate : float = 100f;
public var time : int = 90;
public var speed : float  = 1000;
public var launcher : Transform ;
public var projectile : GameObject;

function Start ()
{
	launcher = transform.FindChild ("launcher");
	player = GameObject.Find("Player");
	destination = player.transform;
	agent = gameObject.GetComponent.<NavMeshAgent>();
	
	agent.SetDestination(destination.position);
}
function Update () 
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
	if(distance <= attackRange)
	{
		attack = true;
	}else{
		attack = false;
	}
	
	if(time > 100 && attack)
	{		
		transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
		var bullet : Rigidbody = Instantiate(launcher,projectile.transform.position, transform.rotation).GetComponent.<Rigidbody>();
		bullet.AddForce(transform.forward * speed);
		time = 0;
	}else if (attack){
		time++;
	}
}
function Dead(){
	enabled = false;
}
