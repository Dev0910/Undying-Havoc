using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{
    [SerializeField] Sprite[] timeImage;
    [SerializeField] Text timeText;
    [SerializeField] float duration, currentTime;
    [SerializeField] private bool day;
    private Image img;

    void Start()
    {
        day = false;
        img = GetComponent<Image>();
        currentTime = duration;
        timeText.text = currentTime.ToString();
        StartCoroutine(StartTimer());
    }


    IEnumerator StartTimer()
    {
        day = !day;
        img.sprite = day ? timeImage[0] : timeImage[1];
        transform.localScale = day ? new Vector3(1, 1, 1) : new Vector3(0.7f, 0.7f, 0.7f);

        while(currentTime >= 0)
        {
            img.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
            timeText.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        currentTime = duration;
        StartCoroutine(StartTimer());
    }
}
