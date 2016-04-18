using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	GameObject parent;

    // Bullet collides with object
	void OnCollisionEnter (Collision collision){
		Debug.Log (collision.gameObject.tag); // Tag of object to debug console
		
        // Test what is hit
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
