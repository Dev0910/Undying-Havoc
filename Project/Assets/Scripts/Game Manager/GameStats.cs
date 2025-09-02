using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStats : MonoBehaviour
{
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
        wood = startWood;
        stone = startStone;
        iron = startIron;
        bone = startBone;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        //score = 0;wood = 0;stone = 0;iron = 0;bone = 0;
        UpdateResourses();

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + score;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
    public void UpdateResourses()
    {
        woodText.text = ": " + wood;
        stoneText.text = ": " + stone;
        ironText.text = ": " + iron;
        boneText.text = ": " + bone;
    }

    //returns true or false after cheaking if the resource are avalable or not
    public bool CheakIfResourseAvailable(EResources resource, int amount)
    {
        bool result = false;
        switch (resource)
        {
            case EResources.None: break;

            case EResources.Wood:
                {
                    result = wood >= amount;
                }
                break;

            case EResources.Stone:
                {
                    result = stone >= amount;
                }
                break;

            case EResources.Bone:
                {
                    result = bone >= amount;
                }
                break;

            case EResources.Iron:
                {
                    result = iron >= amount;
                }
                break;
        }
        return result;
    }
    //add the resources to the games 
    public void AddResourse(EResources resourse, int amount)
    {
        switch (resourse)
        {
            case EResources.None: break;

            case EResources.Wood:
                {
                    wood += amount;
                }
                break;

            case EResources.Stone:
                {
                    stone += amount;
                }
                break;

            case EResources.Bone:
                {
                    bone += amount;
                }
                    break;

            case EResources.Iron:
                {
                    iron += amount;
                }
                break;
            }
        UpdateResourses();

    }
    //removes resources from the game
    public void RemoveResourse(EResources resourse, int amount)
    {
        switch (resourse)
        {
            case EResources.None: break;

            case EResources.Wood:
                {
                    wood -= amount;
                }
                break;

            case EResources.Stone:
                {
                    stone -= amount;
                }
                break;

            case EResources.Bone:
                {
                    bone -= amount;
                }
                break;

            case EResources.Iron:
                {
                    iron -= amount;
                }
                break;
        }
        UpdateResourses();
    }
}
