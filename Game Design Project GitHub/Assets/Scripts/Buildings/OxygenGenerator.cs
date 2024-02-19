using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OxygenGenerator : BuildingBuleprint
{
    [Header("Fuel Related")]
    public GameObject inventoryUI;
    public float waitForSecBeforeStarting;
    public EResources fuelResourse;
    public float timePerRoundinMin;
    public int[] amountPerRound;
    public Image fuelBar;
    public float range;
    public bool isOxygenAreaUp;
    [SerializeField]private GameObject rangeGO;

    private InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        childGameobejct = this.gameObject;
        isOxygenAreaUp = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildingData = buildingScriptableObjects.buildingData;
        currentLevel = 0;
        GetData();
        spriteRenderer.sprite = currentSpriite;
        Invoke("StartFuelUsage", waitForSecBeforeStarting);
        rangeGO = GameObject.Find("Range");
        inventoryUI.SetActive(false);
        inventoryManager = gameObject.GetComponent<InventoryManager>();
    }

    public void UpgradeRange()
    {
        float _range = buildingData[currentLevel].range;
        range = _range;
        rangeGO.transform.localScale = new Vector3(_range, _range, _range);
    }
    private void OnMouseDown()
    {
        if(inventoryUI.activeInHierarchy)
        {
            inventoryUI.SetActive(false);
        }
        else
        {
            inventoryUI.SetActive(true);
        }
    }
    IEnumerator FuelCheak()
    {
        yield return new WaitForSeconds(0.25f);
        if (inventoryManager.currentResource.amount >= amountPerRound[currentLevel] && inventoryManager.currentResource.resource != EResources.None)
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
        isOxygenAreaUp = true;

        inventoryManager.currentResource.amount -= amountPerRound[currentLevel];
        inventoryManager.UpdateDisplayText();
        if(inventoryManager.currentResource.amount <= 0)
        {
            inventoryManager.currentResource.resource = EResources.None;
            inventoryManager.UpdateDisplayImage();
        }
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
        if (isOxygenAreaUp)
        {
            rangeGO.SetActive(false);
            isOxygenAreaUp = false;
        }

        StartCoroutine(FuelCheak());
    }
}
