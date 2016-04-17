#pragma strict

public var parent : GameObject;

public function JumpLand(){
	parent.SendMessage("JumpLand");
	

}

public function JumpFinished(){
	parent.SendMessage("JumpFinished");

}