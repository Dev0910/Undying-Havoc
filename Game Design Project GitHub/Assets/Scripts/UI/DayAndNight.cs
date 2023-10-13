using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DayAndNight : MonoBehaviour
{
    
    private float currentTime = 0f;
    public GameObject nightPanel;
    private Image nightPanelImage;
    public Text timeCycle;
    private int currentAlpha;
    private int changesToAplha;
    public Text currentTimeText;

    public int currentWave;
    public bool isNight = false;
    public float timeBetweenDayAndNight = 15f;
    // Start is called before the first frame update
    void Start()
    {
        currentAlpha = 0;
        currentTime = timeBetweenDayAndNight;
        nightPanelImage = nightPanel.GetComponent<Image>();
        currentWave = 0;
        isNight = false;
        UpdateColor(currentAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        currentTimeText.text = currentTime+"";
        if(currentTime < 0f)
        {
            currentTime = timeBetweenDayAndNight;
            if(isNight)
            {
                timeCycle.text = "Night In : ";
                InvokeRepeating(nameof(MakePanalDisappear), 0, 1);
                //nightPanel.SetActive(false);

            }
            else if(!isNight)
            {
                timeCycle.text = "Day In : ";
                currentWave++;
                InvokeRepeating(nameof(MakePanalVisible), 0, 1);
                GameManager.Instance.spawnManager.SpawnWave();
                //nightPanel.SetActive(true);
            }
            
        }


    }
    private void MakePanalVisible()
    {
        Debug.Log("Trying to make night");
        currentAlpha++;
        if(currentAlpha >= 200)
        {
            isNight = true;
            CancelInvoke();
        }
        else
        {
            UpdateColor(currentAlpha);
        }

    }

    private void MakePanalDisappear()
    {
        Debug.Log("Trying to make Day");
        currentAlpha--;
        if (currentAlpha <= 0)
        {
            isNight = false;
            CancelInvoke();
        }
        else
        {
            UpdateColor(currentAlpha);
        }
    }

    private void UpdateColor(int Alpha)
    {
        nightPanelImage.color = new Color(nightPanelImage.color.r, nightPanelImage.color.g, nightPanelImage.color.b, Alpha/255);
    }
}
