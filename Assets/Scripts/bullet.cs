using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	GameObject parent;

	void OnCollisionEnter (Collision collision){
		Debug.Log (collision.gameObject.tag);
		switch (collision.gameObject.tag) {
			case "tank":
					collision.gameObject.SendMessage("TakeDamage", 20);
				break;

			case "archer":
				if (collision.gameObject != parent) {
					collision.gameObject.SendMessage("TakeDamage", 20);
				}
				break;

			case "mortar":
				collision.gameObject.SendMessage("TakeDamage", 20);
				break;

			case "grunt":
				collision.gameObject.SendMessage("TakeDamage", 20);
				break;

			case "obstacle":
				Destroy(gameObject);
				break;
		}
	}

	void Shot(GameObject parentTemp){
		parent = parentTemp;

	}

}
