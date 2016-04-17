#pragma strict

private var player : GameObject;

function Start () {
	player = GameObject.FindGameObjectWithTag("Player");

}

function Update () {
	transform.position = player.transform.position;

}