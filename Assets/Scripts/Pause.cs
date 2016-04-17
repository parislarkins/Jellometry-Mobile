using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour {

    public bool isPaused;
    public CanvasGroup pausedCanvasGroup;
    public Text txtCountdown;
    public Image timerImage;
    private float timer = 3;
    private int timerLength = 3;
 

	// Use this for initialization
	void Start () {
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {

        // Not quite working, timers won't play while game is paused.
        if (!isPaused && pausedCanvasGroup.alpha == 1 && 0 == 1) {
            timer -= Time.deltaTime;

            string strtemp = (int)(timer) + 1 + "";
            txtCountdown.text = "" + strtemp;
            Debug.Log(strtemp);
            timerImage.fillAmount = (timer % 1.0f);

            if (timer <= 0)
            {
                pausedCanvasGroup.alpha = 0;
                Time.timeScale = 1;
            }
        }
	}

    public void PauseGame() {
        Debug.Log("Paused");

        isPaused = !isPaused;

        if (isPaused)
        {
            //timer = timerLength;
            //timerImage.fillAmount = 0;

            pausedCanvasGroup.alpha = 1;
            Time.timeScale = 0;
        }
        else {
            pausedCanvasGroup.alpha = 0;
            Time.timeScale = 1f;
            //timer = timerLength;
        }
    }
}
