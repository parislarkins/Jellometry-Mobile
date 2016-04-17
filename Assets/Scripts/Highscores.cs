using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Highscores : MonoBehaviour {

    public Text txtName;
    public Text txtHighscore;
    public Text txtTotalKills;

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.HasKey("statUsername"))
            txtName.text = PlayerPrefs.GetString("statUsername");

        if (PlayerPrefs.HasKey("Highscore"))
            txtHighscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();

        if (PlayerPrefs.HasKey("totalEnemiesKilled"))
            txtTotalKills.text = "Total Kills: " + PlayerPrefs.GetInt("totalEnemiesKilled").ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
