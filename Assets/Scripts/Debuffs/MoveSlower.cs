using UnityEngine;
using System.Collections;

public class MoveSlower : MonoBehaviour {

    public float decreaseAmount = 3f;
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
            playerControls.speed -= decreaseAmount;
            playerControls.moveSlowerStart = Time.time;
            Destroy(gameObject);
        }
    }
}
