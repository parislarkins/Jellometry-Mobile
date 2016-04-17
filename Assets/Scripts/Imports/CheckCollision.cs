using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {

    private RaycastHit hit;

	void OnCollisionStay(Collision collision){
		if (collision.gameObject.tag == "obstacle"){
			Debug.Log ("COLLISION HOLFUCK");
			collision.gameObject.GetComponent<Renderer>().sharedMaterial = gameObject.GetComponent<Renderer>().sharedMaterial;
		}
	}

    void Start() {
        //Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 6);
        
        //for (int i = 0; i < hitColliders.Length; i++) {
        //    Debug.Log(gameObject.name + "collided with " + hitColliders[i]);
        //    //if (hitColliders[i].tag == "obstacle") {
        //    //    gameObject.transform.position = new Vector3(Random.Range(-49, 49), gameObject.transform.position.y, Random.Range(-49, 49));
        //    //    int r = UnityEngine.Random.Range(0, 360);
        //    //    gameObject.transform.Rotate(0, r, 0, Space.Self);
        //    //    r = UnityEngine.Random.Range(-4, 0);
        //    //    gameObject.transform.Translate(0, r, 0);
        //    //}
        //}
        //if (Physics.SphereCast(gameObject.transform.position, 14, Vector3.up, out hit, Mathf.Infinity, 8))
        //{
        //    Debug.Log("Colliding with " + hit.collider.gameObject.name);
        //}
    }
}
