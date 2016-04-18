using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Highscores : MonoBehaviour {

    // Initialize
    public Text txtName;
    public Text txtHighscore;
    public Text txtTotalKills;

	// Use this for initialization
	void Start () {
        // Set keys if they don't exist
        if (PlayerPrefs.HasKey("statUsername"))
            txtName.text = PlayerPrefs.GetString("statUsername");

        if (PlayerPrefs.HasKey("Highscore"))
            txtHighscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();

        if (PlayerPrefs.HasKey("totalEnemiesKilled"))
            txtTotalKills.text = "Total Kills: " + PlayerPrefs.GetInt("totalEnemiesKilled").ToString();
	}
	
}
