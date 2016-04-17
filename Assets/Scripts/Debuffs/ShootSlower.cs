using UnityEngine;
using System.Collections;

public class ShootSlower : MonoBehaviour
{

    public float decreaseAmount = .05f;
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
            playerControls.timeBetweenBullets += decreaseAmount;
            playerControls.shootSlowerStart = Time.time;
            Destroy(gameObject);
        }
    }
}