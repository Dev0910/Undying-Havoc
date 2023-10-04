using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DayAndNight : MonoBehaviour
{
    
    private float currentTime = 0f;
    private GameManager gameManager;
    public GameObject nightPanel;
    public Text timeCycle;
    public Text currentTimeText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        currentTime = gameManager.timeBetweenDayAndNight;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        currentTimeText.text = currentTime+"";
        if(currentTime < 0f)
        {
            currentTime = gameManager.timeBetweenDayAndNight;
            if(gameManager.isNight)
            {
                timeCycle.text = "Night In : ";
                gameManager.isNight = false;
                nightPanel.SetActive(false);
            }
            else if(!gameManager.isNight)
            {
                timeCycle.text = "Day In : ";
                gameManager.isNight = true;
                gameManager.currentWave++;
                gameManager.spawnMannager.SpawnWave();
                nightPanel.SetActive(true);
            }
        }
    }
}
