using UnityEngine;
using System.Collections;

public class TankAnimation : MonoBehaviour {
    // Script to be placed meshed child
	public GameObject parent;

	// Use this for initialization
	void Start () {
        // Landing animation
		parent.SendMessage("JumpLand");
	}
	
	// Update is called once per frame
	void Update () {
        // Jump finished
		parent.SendMessage("JumpFinished");
	}
}
