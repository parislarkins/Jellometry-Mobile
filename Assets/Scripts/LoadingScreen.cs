using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	public string levelToLoad;

	public GameObject panel;
	public Text _text;
	public Image progressBar;
    public float animOpenLength = 1.0f;

    private int loadProgress = 0;

    void Start() {
        progressBar.transform.localScale = new Vector3(0, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

    }
	
	// Update is called once per frame
	void Update () {

        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Open"))
            // I dont know wtf is going on but it works like this
            
            StartCoroutine(DisplayLoadingScreen(levelToLoad)); // uncommented
            //StartCoroutine(StartTimer(animOpenLength));


		if (Input.GetKeyDown(KeyCode.Space)){
			StartCoroutine(DisplayLoadingScreen(levelToLoad));
		}
	}

    IEnumerator StartTimer(float length) {

        for (float i = length; i > 0; i -= Time.deltaTime)
            yield return 0;

        StartCoroutine(DisplayLoadingScreen(levelToLoad));
        StopCoroutine("StartTimer");
    }

	IEnumerator DisplayLoadingScreen(string level){
		panel.SetActive (true);

		progressBar.transform.localScale = new Vector3 (loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

		_text.text = "Loading Progress " + loadProgress + "%";

		AsyncOperation async = Application.LoadLevelAsync (level);

		while (!async.isDone) {
			loadProgress = (int)(async.progress * 100);
			_text.text = "Loading Progress " + loadProgress + "%";
            Debug.Log(loadProgress);
			progressBar.transform.localScale = new Vector3 (async.progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

			yield return null;
		}
	}
}
