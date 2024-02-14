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
    public int[] amountPerRound;
    public Image fuelBar;
    public float range;
    private bool isOxygenArea;
    [SerializeField]private GameObject rangeGO;
    // Start is called before the first frame update
    void Start()
    {
        childGameobejct = this.gameObject;
        isOxygenArea = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildingData = buildingScriptableObjects.buildingData;
        currentLevel = 0;
        GetData();
        spriteRenderer.sprite = currentSpriite;
        Invoke("StartFuelUsage", waitForSecBeforeStarting);
        rangeGO = GameObject.Find("Range");
    }

    public void UpgradeRange()
    {
        float _range = buildingData[currentLevel].range;
        range = _range;
        rangeGO.transform.localScale = new Vector3(_range, _range, _range);
    }

    IEnumerator FuelCheak()
    {
        yield return new WaitForSeconds(0.25f);
        if (GameManager.Instance.gameStats.CheakIfResourseAvailable(fuelResourse, amountPerRound[currentLevel]))
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
        rangeGO.SetActive(true);
        GameManager.Instance.gameStats.RemoveResourse(fuelResourse, amountPerRound[currentLevel]);

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
            rangeGO.SetActive(false);
        }

        StartCoroutine(FuelCheak());
    }
}
