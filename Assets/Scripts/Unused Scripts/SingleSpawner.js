#pragma strict

public var enemy : GameObject;
public var maxEnemies : int = 4;
public var numEnemies : int;

var rand : Random;
public var time : int;
var delay : int;

function Start () {

}

function FixedUpdate () {
	if(time >= delay)
	{
		if(numEnemies < maxEnemies)
		{
			Instantiate(enemy,transform.position,transform.rotation);
		}
		numEnemies ++;
		time = 0;
	}
	time++;
}