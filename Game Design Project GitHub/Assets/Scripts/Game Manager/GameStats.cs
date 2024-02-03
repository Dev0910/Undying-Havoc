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

    [Header("Resources")]
    public int wood;
    public Text woodText;
    public int stone;
    public Text stoneText;
    public int iron;
    public Text ironText;

    [Header("Score")]
    public static int score;
    public int highScore;
    public Text scoreText;
    // Start is called before the first frame update

    void Start()
    {
        currentGold = startGold;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        score = 0;wood = 0;stone = 0;iron = 0;
        woodText.text = ": 0";stoneText.text = ": 0";ironText.text = ": 0";

    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = ": " + currentGold;
        scoreText.text = "Score : " + score;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
