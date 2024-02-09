using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OxygenGenerator : BuildingBuleprint
{
    [Header("Fuel Related")]
    public float waitForSecBeforeStarting;
    public EResources fuelResourse;
    public float timePerRoundinMin;
    public int amountPerRound;
    public Image fuelBar;

    private bool isOxygenArea;
    private GameObject range;
    // Start is called before the first frame update
    void Start()
    {
        isOxygenArea = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildingData = buildingScriptableObjects.buildingData;
        currentLevel = 0;
        GetData();
        spriteRenderer.sprite = currentSpriite;
        childGameobejct = this.gameObject;
        Invoke("StartFuelUsage", waitForSecBeforeStarting);
        range = GameObject.Find("Range");
    }

    public void UpgradeRange()
    {
        float _range = buildingData[currentLevel].range;
        range.transform.localScale = new Vector3(_range, _range, _range);
    }

    IEnumerator FuelCheak()
    {
        yield return new WaitForSeconds(0.25f);
        if (CheakIfResourseAvailable(fuelResourse,amountPerRound))
        {
            StartCoroutine(UseFuel());
        }
        else
        {
            StartCoroutine(RemoveOxygenArea());
        }

    }

    void StartFuelUsage()
    {
        StartCoroutine(UseFuel());
    }

    IEnumerator UseFuel()
    {
        range.SetActive(true);
        RemoveResourse(fuelResourse, amountPerRound);

        for(float i = 1; i>=0; i-=0.01f)
        {
            yield return new WaitForSeconds((timePerRoundinMin*60) / 100);
            fuelBar.fillAmount = i;
        }
        //use fuel 
        yield return null;
        StartCoroutine(FuelCheak());
    }

    IEnumerator RemoveOxygenArea()
    {
        //wait for sec
        //then remove Area
        yield return new WaitForSeconds(0.2f);
        if (isOxygenArea)
        {
            range.SetActive(false);
        }

        StartCoroutine(FuelCheak());
    }
}
