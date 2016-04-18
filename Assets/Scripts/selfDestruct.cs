using UnityEngine;
using System.Collections;

public class selfDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5);
	}
}
