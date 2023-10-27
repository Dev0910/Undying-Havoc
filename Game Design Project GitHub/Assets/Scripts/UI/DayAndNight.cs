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
    //public Text timeCycle;
    private float currentAlpha;
    private int changesToAplha;
    //public Text currentTimeText;

    [Header("Time Animation")]
    [SerializeField] Sprite[] timeImage;
    [SerializeField]private Image currentImage;

    public int currentWave;
    public bool isNight = false;
    public float timeBetweenDayAndNight = 15f;
    // Start is called before the first frame update
    void Start()
    {
        currentAlpha = 0;
        currentImage = currentImage.GetComponent<Image>();
        currentTime = timeBetweenDayAndNight;
        nightPanelImage = nightPanel.GetComponent<Image>();
        currentWave = 0;
        isNight = true;
        UpdateColor();
        StartCoroutine(StartTimer());
    }
    private void MakeNight()
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

    private void MakeDay()
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

    IEnumerator StartTimer()
    {
        
        PanalAnimation();
        currentImage.sprite = isNight ? timeImage[0] : timeImage[1];

        while (currentTime >= 0)
        {
            currentImage.fillAmount = Mathf.InverseLerp(0, timeBetweenDayAndNight, currentTime);
            yield return new WaitForSeconds(0.1f);
            currentTime -= 0.1f;
        }
        currentTime = timeBetweenDayAndNight;
        StartCoroutine(StartTimer());
    }

    private void PanalAnimation()
    {
        if (isNight)
        {
            InvokeRepeating(nameof(MakeDay), 0, 0.01f);
            isNight = false;
            PlayerController playerController = GameManager.Instance.player.GetComponent<PlayerController>();
            playerController.RegenrateHealth(timeBetweenDayAndNight);
        }
        else
        {
            currentWave++;
            InvokeRepeating(nameof(MakeNight), 0, 0.01f);
            isNight = true;
            GameManager.Instance.spawnManager.SpawnWave();
        }
    }
}
