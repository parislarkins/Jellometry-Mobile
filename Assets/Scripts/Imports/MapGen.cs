using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour {
	public GameObject[] obstacles;
	public Material[] materials;
	public Renderer[] renderers;
	private float r;
	
	void Start() {
        obstacles = GameObject.FindGameObjectsWithTag("obstacle");
		GenerateLevel ();
	}
	void GenerateLevel(){
		for (int i = 0; i < GameObject.FindGameObjectsWithTag("obstacle").Length; i++){
            obstacles[i].transform.position = new Vector3(Random.Range(-47, 47), obstacles[i].transform.position.y, Random.Range(-47, 47));
			obstacles[i].GetComponent<Renderer>().sharedMaterial = materials[UnityEngine.Random.Range(0,5)];
			r = UnityEngine.Random.Range(0,360);
			obstacles[i].transform.Rotate (0,r,0,Space.Self);
			r = UnityEngine.Random.Range(-4,0);
			obstacles[i].transform.Translate(0,r,0);
		}
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("obstacle").Length; i++) {
            Collider[] hitColliders = Physics.OverlapSphere(obstacles[i].transform.position, 4);
            Debug.Log(hitColliders.Length);
            do{
                for (int j = 0; j < hitColliders.Length; j++)
                {
                    if (hitColliders[j].name != "Floor" && hitColliders[j].name != obstacles[i].name)
                    {
                        Debug.Log(obstacles[i] + "collided with " + hitColliders[j]);
                        obstacles[i].transform.position = new Vector3(Random.Range(-47, 47), obstacles[i].transform.position.y, Random.Range(-47, 47));
                        r = UnityEngine.Random.Range(0, 360);
                        obstacles[i].transform.Rotate(0, r, 0, Space.Self);
                        if (obstacles[i].transform.position.y > -2)
                        {
                            r = UnityEngine.Random.Range(-4, 0);
                            obstacles[i].transform.Translate(0, r, 0);
                        }
                    }
                }
                hitColliders = Physics.OverlapSphere(obstacles[i].transform.position, 4);
                Debug.Log(hitColliders.Length);
            }while(hitColliders.Length > 2);

        }
	}
}