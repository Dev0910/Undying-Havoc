using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[System.Serializable]
public class BuildingBuleprint : MonoBehaviour
{
    [Header("Hover UI")]
    [SerializeField] GameObject hoverUI;

    [Header("Unity Things")]
    public GameObject childGameobejct;
    public BuildingScriptableObjects buildingScriptableObjects;
    public Sprite upgradedSprite;

    [Header("Details")]
    public float currentHealth = 0f;
    public int currentLevel = 0;

    //[Header("Money")]
    //public int cost;
    //public int sellPrice;
    //public int upgradeCost;

    [Header("Resourse Required")]
    public List<SingleResourse> resourseListToBuyBuilding = new List<SingleResourse>();

    [Header("Attack")]
    public int damage;


    [Header("Particle Effect")]
    public GameObject destructionEffect;

    protected GameObject nearestTile = null;
    protected SpriteRenderer spriteRenderer;
    protected BuildingData[] buildingData;
    protected Sprite currentSpriite;
    protected Sprite currentBulletSprite;


    //private void OnMouseDown()
    //{
    //    //to sell the building 
    //    if (ClickHandler.xDown && buildingData[currentLevel].sellResourseList.Count > 0)//taking refrence from the click Handler
    //    {
    //        SellBuilding();
    //    }

    //    //to upgrade the building
    //    if (ClickHandler.vDown && CheakIfResourseAvailable(currentLevel) && buildingData[currentLevel].resourseList.Count > 0)
    //    {
    //        UpgradeBuilding();
    //    }
    //}

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (ClickHandler.xDown && buildingData[currentLevel].sellResourseList.Count > 0)//taking refrence from the click Handler
            {
                SellBuilding();
            }

            //to upgrade the building
            if (ClickHandler.vDown && CheakIfResourseAvailable(currentLevel) && buildingData[currentLevel].resourseList.Count > 0)
            {
                UpgradeBuilding();
            }
        }

    }

    //function is called when building takes damage
    public void TakeDamage(float Damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= Damage;
        }
        else if(currentHealth <= 0)
        {
            nearestTile.GetComponent<Tile>().isOccupied = false;//change the is Occuied bool in the class Tile
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
            GameObject p = Instantiate(destructionEffect,transform.position, Quaternion.identity);
            GameObject.Destroy(p,0.5f);
        }
    }

    void SellBuilding()
    {
        AddResourse();//adding the selling price of the turret 
        Destroy(this.gameObject);//destroying the building

        //change the isOccuied bool in the class Tile
        if (nearestTile != null && nearestTile.GetComponent<Tile>().isOccupied == true)
        {
            nearestTile.GetComponent<Tile>().isOccupied = false;
        }
    }
    void UpgradeBuilding()
    {
        RemoveResourse(currentLevel);//removing the upgrade cost
        currentLevel++;
        GetData();
        spriteRenderer.sprite = currentSpriite;
    }


    //Get data from the scriptable object
    protected void GetData()
    {
        currentSpriite = buildingData[currentLevel].buildingSprite;
        currentBulletSprite = buildingData[currentLevel].bulletSprite;
        currentHealth = buildingData[currentLevel].health;
        damage = buildingData[currentLevel].damage;
        if (currentLevel < buildingData.Length - 1)
        {
            upgradedSprite = buildingData[currentLevel + 1].buildingSprite;
        }
        else
        {
            //Display Max Level
        }
        
        if (buildingData[currentLevel].range > 0 && childGameobejct != null)
        {
            
            if (childGameobejct.name == "Oxygen Generator")
            {

                childGameobejct.GetComponent<OxygenGenerator>().UpgradeRange();

            }
        }
        
    }

    protected void OnMouseEnter()
    {
        hoverUI.SetActive(true);
    }

    protected void OnMouseExit()
    {
        hoverUI.SetActive(false);
    }

    #region Resourse Calculations
    public bool CheakIfResourseAvailable(int index)
    {
        GameStats gs = GameManager.Instance.gameStats;
        bool result = false;

        foreach (SingleResourse resourse in buildingData[index].resourseList)
        {
            switch(resourse.resource)
            {
                case EResources.None: break;

                case EResources.Wood:
                {
                        result = gs.wood >= resourse.amount;
                }break;

                case EResources.Stone:
                    {
                        result = gs.stone >= resourse.amount;
                    }
                    break;

                case EResources.Bone:
                    {
                        result = gs.bone >= resourse.amount;
                    }
                    break;

                case EResources.Iron:
                    {
                        result = gs.iron >= resourse.amount;
                    }
                    break;
            }
        }
        return result;
    }

    public void AddResourse()
    {
        GameStats gs = GameManager.Instance.gameStats;
        foreach (SingleResourse resourse in buildingData[currentLevel].sellResourseList)
        {
            int sellAmount = resourse.amount;
            switch (resourse.resource)
            {
                case EResources.None: break;

                case EResources.Wood:
                    {
                        gs.wood += sellAmount;
                    }
                    break;

                case EResources.Stone:
                    {
                        gs.stone += sellAmount;
                    }
                    break;

                case EResources.Bone:
                    {
                        gs.bone += sellAmount;
                    }
                    break;

                case EResources.Iron:
                    {
                        gs.iron += sellAmount;
                    }
                    break;
            }
            gs.UpdateResourses();
        }
    }

    public void RemoveResourse(int index)
    {
        GameStats gs = GameManager.Instance.gameStats;
        foreach (SingleResourse resourse in buildingData[index].resourseList)
        {
            switch (resourse.resource)
            {
                case EResources.None: break;

                case EResources.Wood:
                    {
                        gs.wood -= resourse.amount;
                    }
                    break;

                case EResources.Stone:
                    {
                        gs.stone -= resourse.amount;
                    }
                    break;

                case EResources.Bone:
                    {
                        gs.bone -= resourse.amount;
                    }
                    break;

                case EResources.Iron:
                    {
                        gs.iron -= resourse.amount;
                    }
                    break;
            }
            gs.UpdateResourses();
        }
    }
    public void RemoveResourse()
    {
        GameStats gs = GameManager.Instance.gameStats;
        foreach (SingleResourse resourse in resourseListToBuyBuilding)
        {
            switch (resourse.resource)
            {
                case EResources.None: break;

                case EResources.Wood:
                    {
                        gs.wood -= resourse.amount;
                    }
                    break;

                case EResources.Stone:
                    {
                        gs.stone -= resourse.amount;
                    }
                    break;

                case EResources.Bone:
                    {
                        gs.bone -= resourse.amount;
                    }
                    break;

                case EResources.Iron:
                    {
                        gs.iron -= resourse.amount;
                    }
                    break;
            }
            gs.UpdateResourses() ;
        }
    }

    
    #endregion
}
