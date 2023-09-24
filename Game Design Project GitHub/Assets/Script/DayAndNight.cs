using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DayAndNight : MonoBehaviour
{
    public float timeToDayOrNight = 15f;
    private float currentTime = 0f;
    bool isNight = false;
    public GameObject nightPanel;
    public Text timeCycle;
    public Text currentTimeText;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = timeToDayOrNight;
        isNight = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        currentTimeText.text = currentTime+"";
        if(currentTime < 0f)
        {
            currentTime = timeToDayOrNight;
            if(isNight )
            {
                timeCycle.text = "Night In : ";
                isNight = false;
                nightPanel.SetActive(false);
            }
            else if(!isNight)
            {
                timeCycle.text = "Day In : ";

                isNight = true;
                nightPanel.SetActive(true);
            }
        }
    }
}
