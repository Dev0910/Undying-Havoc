using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DayAndNight : MonoBehaviour
{
    
    private float currentTime = 0f;
    public float speedOfAnimation = 1f;
    public GameObject nightPanel;
    private Image nightPanelImage;
    public Text timeCycle;
    private float currentAlpha;
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
        UpdateColor();
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
                InvokeRepeating(nameof(MakePanalDisappear), 0, 0.01f);
                Debug.Log("Trying to make Day");
                //nightPanel.SetActive(false);

            }
            else if(!isNight)
            {
                timeCycle.text = "Day In : ";
                currentWave++;
                InvokeRepeating(nameof(MakePanalVisible), 0, 0.01f);
                GameManager.Instance.spawnManager.SpawnWave();
                Debug.Log("Trying to make night");
                //nightPanel.SetActive(true);
            }
            
        }


    }
    private void MakePanalVisible()
    {
        currentAlpha += speedOfAnimation;
        if(currentAlpha < 200)
        {
            UpdateColor();
        }
        else
        {
            isNight = true;
            CancelInvoke();
        }

    }

    private void MakePanalDisappear()
    {
        currentAlpha -= speedOfAnimation;
        if (currentAlpha > 0)
        {
            UpdateColor();
        }
        else
        {
            isNight = false;
            CancelInvoke();
        }
    }

    private void UpdateColor()
    {
        nightPanelImage.color = new Color(nightPanelImage.color.r, nightPanelImage.color.g, nightPanelImage.color.b, currentAlpha/255);
    }
}
