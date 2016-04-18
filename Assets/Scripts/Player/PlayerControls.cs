using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{

    // ----------------------
    // User Made Variables
    // ----------------------

    // Movement
    public float speed = 10;
    public float moveSpeed = 10;
    private float distToGround; // Distance from player to ground
    private ParticleSystem movementParticles;   // Reference to the particle system

    // Gun
    public string weapon = "pistol";
    public int damage;
    public int damagePerShot;                  // The damage inflicted by each bullet.
    public float timeBetweenShots;        // The time between each shot.
    public float timeBetweenBullets;
    public float range;                      // The distance the gun can fire.

    private float timer;
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootableMask;
    private ParticleSystem gunParticles;

    public GameObject hitParticles;
    public ParticleSystem bulletParticles;

    // Animator
    private Animator playerAnimator;            // Reference to the animator component.

    // Testing
    private GameObject moveDirBlock;

    public float shootFasterStart = 0;
    public float shootFasterDuration = 10;

    public float moveFasterStart = 0;
    public float moveFasterDuration = 10;

    public float moveSlowerStart = 0;
    public float moveSlowerDuration = 10;

    public float shootSlowerStart = 0;
    public float shootSlowerDuration = 10;

    public float reduceDamageStart = 0;
    public float reduceDamageDuration = 10;



    // ----------------------
    // Start()
    // ----------------------

    void Start()
    {
        SelectWeapon("pistol");

        timeBetweenBullets = timeBetweenShots;
        // Set up references.
        playerAnimator = GetComponentInChildren<Animator>();
        movementParticles = GameObject.Find("WalkingParticles").GetComponent<ParticleSystem>();

        // Gun
        gunParticles = gameObject.GetComponentInChildren<ParticleSystem>();
        shootableMask = LayerMask.GetMask("Shootable");

        // Testing
        moveDirBlock = GameObject.FindGameObjectWithTag("MoveDir");
        distToGround = transform.GetComponent<Collider>().bounds.extents.y;

    }

    // ----------------------
    // FixedUpdate()
    // ----------------------

    void FixedUpdate()
    {

        if (shootFasterStart > 0 && Time.time - shootFasterStart >= shootFasterDuration)
        {
            timeBetweenBullets += 0.05f;
            shootFasterStart = 0;
        }
        if (moveFasterStart > 0 && Time.time - moveFasterStart >= moveFasterDuration)
        {
            speed -= 3;
            moveFasterStart = 0;
        }
        if (moveSlowerStart > 0 && Time.time - moveSlowerStart >= moveSlowerDuration)
        {
            speed += 3;
            moveSlowerStart = 0;
        }
        if (shootSlowerStart > 0 && Time.time - shootSlowerStart >= shootSlowerDuration)
        {
            timeBetweenBullets -= 0.05f;
            shootSlowerStart = 0;
        }
        if (reduceDamageStart > 0 && Time.time - reduceDamageStart >= reduceDamageDuration)
        {
            damagePerShot += 5;
            reduceDamageStart = 0;
        }
        // Gun Timer
        timer += Time.deltaTime;

        float speedChanged = speed * Time.deltaTime;

        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * speedChanged);
                checkGround();
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * speedChanged);
                checkGround();
            }
            if (Input.GetKey(KeyCode.S))
            {
				transform.Translate(-Vector3.forward * speedChanged);
                checkGround();
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * speedChanged);
                checkGround();
            }
            if (Input.GetKey("space"))
            {
                if (timer >= timeBetweenBullets)
                {
                    Shoot();
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (timer >= timeBetweenBullets)
                {
                    Shoot();
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            WalkingParticles(false, 0f);
            Animating(false);
        }
    }

    void checkGround()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        {
            WalkingParticles(true, 90f); // Activates walking particles if moving
            Animating(true); // Activates animating
        }
        else
        {
            WalkingParticles(false, 0f);
            Animating(false);
        }
    }

    void WalkingParticles(bool walking, float num)
    {

        // Only activates if 
        if (num > 0 && walking)
        {
            movementParticles.Play();
        }
        else
        {
            movementParticles.Stop();
        }
    }

    void Animating(bool walking)
    {
        // Tell the animator whether or not the player is walking.
        playerAnimator.SetBool("IsWalking", walking);

    }

    // TESTING
    void Shoot()
    {
        switch (weapon)
        {
            case "pistol":
                // Reset the timer.
                timer = 0f;

                // Stop the particles from playing if they were, then start the particles.
                gunParticles.Stop();
                gunParticles.Play();

                // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
                shootRay.origin = transform.position;
                shootRay.direction = transform.forward;

                // Perform the raycast against gameobjects on the shootable layer and if it hits something...
                if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
                {
                    if (shootHit.collider.gameObject.tag == "obstacle")
                    {
                        //    		var particles : ParticleSystem = Instantiate(hitParticles,shootHit.point,shootHit.transform.rotation).GetComponent.<ParticleSystem>();
                        //   			var hitColor : Color = shootHit.collider.GetComponent.<Renderer>().material.GetColor("_Color");
                        //   			particles.GetComponent.<Renderer>().material.SetColor("_TintColor",hitColor);

                        GameObject partSysObj;
                        partSysObj = Instantiate(hitParticles, shootHit.point, Quaternion.identity) as GameObject;
                        ParticleSystem partSys = partSysObj.GetComponent<ParticleSystem>();
                        Color hitColor = shootHit.collider.GetComponent<Renderer>().material.GetColor("_Color");
                        partSys.GetComponent<Renderer>().material.SetColor("_TintColor", hitColor);

                        Vector3 dirVector = new Vector3(shootHit.collider.gameObject.transform.position.x, partSys.transform.position.y, shootHit.collider.gameObject.transform.position.z);
                        partSysObj.transform.LookAt(dirVector);
                        //partSysObj.transform.rotation.eulerAngles.y += 180;
                        partSysObj.transform.RotateAround(shootHit.point, Vector3.up, 180f);

                        float distance = Vector3.Distance(transform.position, shootHit.point);
                        float time = distance / bulletParticles.startSpeed;

                        StartCoroutine(Wait(time, partSys));
                    }

                    //Try and find an EnemyHealth script on the gameobject hit.
                    EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                    // If enemyHealth exists, take damage
                    if (enemyHealth != null)
                    {
                        // Calc distance and time
                        float distance = Vector3.Distance(transform.position, shootHit.point);
                        float time = distance / bulletParticles.startSpeed;

                        // Start coroutine to wait for bullet travel
                        StartCoroutine(WaitEnemy(time, enemyHealth));
                    }
                }
                break;
        }

    }

    IEnumerator Wait(float time, ParticleSystem partSys)
    {
        yield return new WaitForSeconds(time);

        // Check if it hasn't already been destroyed
        if (partSys != null)
            partSys.Play();
    }

    IEnumerator WaitEnemy(float time, EnemyHealth enemyHealth)
    {
        yield return new WaitForSeconds(time);

        // Check if still exists
        if (enemyHealth != null)
            enemyHealth.TakeDamage(damagePerShot, shootHit.point);

    }

    void SelectWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "pistol":
                weapon = "pistol";
                damage = 20;
                damagePerShot = 20;
                timeBetweenShots = 0.15f;
                timeBetweenBullets = 0.15f;
                range = 100f;
                //change model/texture/particles n shit?
                break;
            case "shotgun":
                weapon = "shotgun";
                break;
            case "tripleshot":
                weapon = "tripleshot";
                break;
            case "rocket":
                weapon = "rocket";
                break;
            case "flamethrower":
                weapon = "flamethrower";
                break;
            case "grenadelauncher":
                weapon = "grenadelauncher";
                break;
            case "rifle":
                weapon = "rifle";
                break;
        }
    }
}
