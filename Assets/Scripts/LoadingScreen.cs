using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
    
    // Initializing Variables 
    public string levelToLoad;
	public Text _text; // Text for loading progress
	public Image progressBar; // Image for loading progress
    private int loadProgress = 0;

    void Start() {
        // Reset image x scale to 0
        progressBar.transform.localScale = new Vector3(0, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
        StartCoroutine(DisplayLoadingScreen(levelToLoad)); // Start loading the level

    }

    // Corourtine for loading the next scene
	IEnumerator DisplayLoadingScreen(string level){

        // Initialize Loading Percentage text and image
		progressBar.transform.localScale = new Vector3 (loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
		_text.text = "Loading Progress " + loadProgress + "%"; 

		AsyncOperation async = Application.LoadLevelAsync (level); // Unity Magic

        // While not finished,
		while (!async.isDone) {
			loadProgress = (int)(async.progress * 100);
			_text.text = "Loading Progress " + loadProgress + "%"; // Update Load Perc

            // Update progress bar width
			progressBar.transform.localScale = new Vector3 (async.progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

			yield return null;
		}
	}
}
