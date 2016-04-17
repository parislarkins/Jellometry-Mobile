using UnityEngine;
using System.Collections;

public class RandomColour : MonoBehaviour {
	public GameObject[] obstacles;
	public Material[] materials;
	public Renderer[] renderers;
	
	void Start() {
		for (int i = 0; i < GameObject.FindGameObjectsWithTag("obstacle").Length - 1; i++){
			GameObject[] obstacles = GameObject.FindGameObjectsWithTag("obstacle");
			//obstacles[i].transform.position = new Vector3(Random.Range(-408,-398)/10,obstacles[i].transform.position.y,Random.Range (2663,2673)/100);
			obstacles[i].GetComponent<Renderer>().sharedMaterial = materials[UnityEngine.Random.Range(0,4)];
			//obstacles[i].transform.rotation.y = UnityEngine.Random.Range (0,360);
		}
	}
}