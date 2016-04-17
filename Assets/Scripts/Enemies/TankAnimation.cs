using UnityEngine;
using System.Collections;

public class TankAnimation : MonoBehaviour {

	public GameObject parent;

	// Use this for initialization
	void Start () {
		parent.SendMessage("JumpLand");
	}
	
	// Update is called once per frame
	void Update () {
		parent.SendMessage("JumpFinished");
	}
}
