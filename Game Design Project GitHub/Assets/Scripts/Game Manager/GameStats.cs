using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStats : MonoBehaviour
{
    [Header("Gold")]
    public int startGold = 1000;
    public static int currentGold;
    public Text goldText;

    [Header("Score")]
    public static int score;
    public int highScore;
    public Text scoreText;
    // Start is called before the first frame update

    void Start()
    {
        currentGold = startGold;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = " : " + currentGold;
        scoreText.text = "Wave : " + score;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
