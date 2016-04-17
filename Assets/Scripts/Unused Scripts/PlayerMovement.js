#pragma strict

public var speed : float = 6f;            // The speed that the player will move at.
public var jumpHeight : float = 10f;
private var canJump : boolean = true;

private var movement : Vector3;                   // The vector to store the direction of the player's movement.
private var playerAnimator : Animator;                      // Reference to the animator component.
private var playerRigid : Rigidbody;          // Reference to the player's rigidbody.
private var floorMask : int;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
private var camRayLength : float = 1000f;          // The length of the ray from the camera into the scene.

private var movementParticles : ParticleSystem;

function Awake ()
{
    // Create a layer mask for the floor layer.
    floorMask = LayerMask.GetMask ("Floor");

    // Set up references.
    playerAnimator = GetComponentInChildren(Animator);
    playerRigid = GetComponent (Rigidbody);
    var moveTemp : GameObject = GameObject.Find("WalkingParticles");
    movementParticles = moveTemp.GetComponent.<ParticleSystem>();
}


function FixedUpdate ()
{
	var multiplier : float = 0.5;
    // Store the input axes.
    // Horizontal - Left/Right | Vertical - Forward/Backwards
    var h : float = Input.GetAxisRaw ("Horizontal");
    var v : float = Input.GetAxisRaw ("Vertical");
    
    WalkingParticles(h, v);
    
    var speedChanged : float;
    
    if (canJump){
    	speedChanged = speed;
    }
    else{
    	speedChanged = speed / 2;    
    }
    
    // Forward and Backwards
    if (v > 0){
    	transform.localPosition.x += speedChanged * Time.deltaTime;
    	transform.localPosition.z += speedChanged * Time.deltaTime;
    
    }
    else if (v < 0){
    	transform.localPosition.x -= speedChanged * Time.deltaTime;
    	transform.localPosition.z -= speedChanged * Time.deltaTime;
    
    }
    
    // Left and Right
    if (h > 0){
    	transform.localPosition.x += speedChanged * Time.deltaTime;
    	transform.localPosition.z -= speedChanged * Time.deltaTime;
    
    }
    else if (h < 0){
    	transform.localPosition.x -= speedChanged * Time.deltaTime;
    	transform.localPosition.z += speedChanged * Time.deltaTime;
    
    }
    
    // Jump
    if (Input.GetKeyDown(KeyCode.Space) && canJump){
    	Jump();
    	canJump = false;
    
    }

    // Turn the player to face the mouse cursor.
    Turning ();

    // Animate the player.
    Animating (h, v);
}

function WalkingParticles(h : int, v : int){
	if (!canJump){
		movementParticles.Stop();	
	}
	else if (h != 0f || v!= 0f){
		movementParticles.Play();	
	}
	else{ // Idle
		movementParticles.Stop();	
	}

}

function Jump (){
	playerRigid.AddForce(transform.up * jumpHeight);

}

function OnCollisionEnter(other : Collision){
	if (other.transform.tag == "Floor"){
		canJump = true;
	
	}
}

function Turning ()
{
    // Create a ray from the mouse cursor on screen in the direction of the camera.
    var camRay : Ray = Camera.main.ScreenPointToRay (Input.mousePosition);

    // Create a RaycastHit variable to store information about what was hit by the ray.
    var floorHit : RaycastHit;

    // Perform the raycast and if it hits something on the floor layer...
    if(Physics.Raycast (camRay, floorHit, camRayLength, floorMask))
    {
        // Create a vector from the player to the point on the floor the raycast from the mouse hit.
        var playerToMouse : Vector3  = floorHit.point - transform.position;

        // Ensure the vector is entirely along the floor plane.
        playerToMouse.y = 0f;

        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
        var newRotation : Quaternion = Quaternion.LookRotation (playerToMouse);

        // Set the player's rotation to this new rotation.
        playerRigid.MoveRotation (newRotation);
    }
}


function Animating (h : float, v : float)
{

	playerAnimator.SetBool("IsJumping", !canJump);
	
	// Create a boolean that is true if either of the input axes is non-zero.
    var walking : boolean = h != 0f || v != 0f;

    // Tell the animator whether or not the player is walking.
    playerAnimator.SetBool ("IsWalking", walking);
	
}