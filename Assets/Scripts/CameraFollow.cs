﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
        // Find player
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        // Follow player
		transform.position = player.transform.position;
	}
}
