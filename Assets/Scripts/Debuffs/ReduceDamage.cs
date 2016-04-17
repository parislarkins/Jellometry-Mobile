using UnityEngine;
using System.Collections;

public class ReduceDamage : MonoBehaviour {

    public int decreaseAmount = 5;
    private GameObject Player;
    private PlayerControls playerControls;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        playerControls = Player.GetComponent<PlayerControls>();
    }
    void OnTriggerEnter(Collider thing)
    {
        if (thing.gameObject.tag == "Player")
        {
            playerControls.damagePerShot -= decreaseAmount;
            playerControls.reduceDamageStart = Time.time;
            Destroy(gameObject);
        }
    }
}
