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

    [Header("Wood")]
    [SerializeField] int startWood;
    public int wood;
    public Text woodText;

    [Header("Stone")]
    [SerializeField] int startStone;
    public int stone;
    public Text stoneText;

    [Header("Iron")]
    [SerializeField] int startIron;
    public int iron;
    public Text ironText;

    [Header("Bone")]
    [SerializeField] int startBone;
    public int bone;
    public Text boneText;

    [Header("Score")]
    public static int score;
    public int highScore;
    public Text scoreText;
    // Start is called before the first frame update

    void Start()
    {
        currentGold = startGold;
        wood = startWood;
        stone = startStone;
        iron = startIron;
        bone = startBone;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        //score = 0;wood = 0;stone = 0;iron = 0;bone = 0;
        woodText.text = ": " + wood; stoneText.text = ": " + stone; ironText.text = ": " + iron;boneText.text = ": " + bone;

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
