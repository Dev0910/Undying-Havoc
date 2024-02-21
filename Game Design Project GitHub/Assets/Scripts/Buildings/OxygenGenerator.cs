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

    //caleed by the base class when the building is upgraded to upgrade the range of the oxygen area
    public void UpgradeRange()
    {
        float _range = buildingData[currentLevel].range;
        range = _range;
        rangeGO.transform.localScale = new Vector3(_range, _range, _range);
    }
    //private void OnMouseDown()
    //{
    //    if(inventoryUI.activeInHierarchy)
    //    {
    //        inventoryUI.SetActive(false);
    //    }
    //    else
    //    {
    //        inventoryUI.SetActive(true);
    //    }
    //}
    //Cheaks if there is fuel in the oxygen genrataor 
    IEnumerator FuelCheak()
    {
        yield return new WaitForSeconds(0.25f);
        //if the is fuel the is will start using it and keep the oxygen Area up
        if (inventoryManager.currentResource.amount >= amountPerRound[currentLevel] && inventoryManager.currentResource.resource != EResources.None)
        {
            StartCoroutine(UseFuel());
        }
        //else the oxygen area will shut down
        else
        {
            StartCoroutine(RemoveOxygenArea());
        }

    }
    //Initaly called by the start function just to give it a delat
    void StartFuelUsage()
    {
        StartCoroutine(UseFuel());
    }

    //use the resource and keeps the oxygen area up for a selected time
    IEnumerator UseFuel()
    {
        rangeGO.SetActive(true);//sets the oxygen area Gameobject True
        isOxygenAreaUp = true;

        inventoryManager.currentResource.amount -= amountPerRound[currentLevel];//takes the resource from the inventory
        inventoryManager.UpdateDisplayText();
        //reset the inventory if there is no resource
        if(inventoryManager.currentResource.amount <= 0)
        {
            inventoryManager.currentResource.resource = EResources.None;
            inventoryManager.UpdateDisplayImage();
        }
        //runs the loop 100 times
        for(float i = 1; i>=0; i-=0.01f)
        {
            yield return new WaitForSeconds((timePerRoundinMin*60) / 100);//first convert the time in seconds and then divid it by 100
            fuelBar.fillAmount = i;//update the fuel bar
        }
        //use fuel 
        yield return null;
        StartCoroutine(FuelCheak());//loop back to the Fuel Cheak
    }

    //removes the oxygen area in case of insufecient resources
    IEnumerator RemoveOxygenArea()
    {
        //wait for sec every time the fuction is called
        yield return new WaitForSeconds(0.2f);

        //set the oxygen are Gameobject false if it is true
        if (isOxygenAreaUp)
        {
            rangeGO.SetActive(false);
            isOxygenAreaUp = false;
        }
        //loop back to the fuel cheak
        StartCoroutine(FuelCheak());
    }
}
