using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : " + GameStats.score;
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore");
    }
}
