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
    public float currentMaxHealth;
    public int currentLevel = 0;

    //[Header("Money")]
    //public int cost;
    //public int sellPrice;
    //public int upgradeCost;

    [Header("Resourse Required")]
    public SingleResourse resourceRequiredToBuild;

    [Header("Attack")]
    public int damage;


    [Header("Particle Effect")]
    public GameObject destructionEffect;

    protected GameObject nearestTile = null;
    protected SpriteRenderer spriteRenderer;
    protected BuildingData[] buildingData;
    protected Sprite currentSpriite;
    protected Sprite currentBulletSprite;
    protected string currentName;
    private void OnMouseDown()
    {
        //to sell the building 
        if (ClickHandler.xDown && buildingData[currentLevel].sellResourse.resource != EResources.None)//taking refrence from the click Handler
        {
            SellBuilding();
        }

        //to upgrade the building
        if (ClickHandler.vDown && CheakIfResourseAvailable(currentLevel) && buildingData[currentLevel].resourse.resource != EResources.None)
        {
            UpgradeBuilding();
        }
    }

    //function is called when building takes damage
    public void TakeDamage(float Damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= Damage;
            hoverUI.GetComponent<HoverUI>().UpdateHealthBar(currentHealth / currentMaxHealth);
        }

        else if(currentHealth <= 0)
        {
            if (childGameobejct.name == "Oxygen Generator")
            {
                Time.timeScale = 0f;
                GameManager.Instance.mainScreenUI.LoadScene("EndScene");
            }
            else 
            {
                nearestTile.GetComponent<Tile>().isOccupied = false;//change the is Occuied bool in the class Tile
                Destroy(this.gameObject);
                GameObject p = Instantiate(destructionEffect, transform.position, Quaternion.identity);
                GameObject.Destroy(p, 0.5f);
            }

        }
    }

    public void SellBuilding()
    {
        AddResourse();//adding the selling price of the turret 
        //change the isOccuied bool in the class Tile
        if (nearestTile != null && nearestTile.GetComponent<Tile>().isOccupied == true)
        {
            nearestTile.GetComponent<Tile>().isOccupied = false;
        }
        Destroy(this.gameObject);//destroying the building
    }
    public void UpgradeBuilding()
    {
        RemoveResourse(currentLevel);//removing the upgrade cost
        currentLevel++;
        GetData();
        spriteRenderer.sprite = currentSpriite;
    }


    //Get data from the scriptable object
    protected void GetData()
    {
        HoverUI hoverUIScript = hoverUI.GetComponent<HoverUI>();

        currentName = buildingData[currentLevel].name;
        hoverUIScript.UpdateName(currentName);
        hoverUIScript.UpdateDetails(buildingData[currentLevel].description);

        currentSpriite = buildingData[currentLevel].buildingSprite;
        currentBulletSprite = buildingData[currentLevel].bulletSprite;

        currentMaxHealth = buildingData[currentLevel].health;
        currentHealth = currentMaxHealth;
        hoverUI.GetComponent<HoverUI>().UpdateHealthBar(currentHealth / currentMaxHealth);

        damage = buildingData[currentLevel].damage;

        
        if (currentLevel < buildingData.Length - 1)
        {
            upgradedSprite = buildingData[currentLevel + 1].buildingSprite;
            hoverUIScript.UpdateUpgradeCost(buildingData[currentLevel].resourse.resource, buildingData[currentLevel].resourse.amount);
        }
        else
        {
            //Display Max Level
        }
        
        if (childGameobejct.name == "Oxygen Generator")
        {
            childGameobejct.GetComponent<OxygenGenerator>().UpgradeRange();
        }
        else
        {
            hoverUIScript.UpdateSellingPrice(buildingData[currentLevel].sellResourse.resource, buildingData[currentLevel].sellResourse.amount);
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
        SingleResourse resourse = buildingData[index].resourse;
        
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
        return result;
    }

    public void AddResourse()
    {
        GameStats gs = GameManager.Instance.gameStats;
        SingleResourse resourse = buildingData[currentLevel].sellResourse;

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

    public void RemoveResourse(int index)
    {
        GameStats gs = GameManager.Instance.gameStats;
        SingleResourse resourse = buildingData[index].resourse;
        
        
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
    public void RemoveResourse()
    {
        GameStats gs = GameManager.Instance.gameStats;
        SingleResourse resource = resourceRequiredToBuild;
        
            switch (resource.resource)
            {
                case EResources.None: break;

                case EResources.Wood:
                    {
                        gs.wood -= resource.amount;
                    }
                    break;

                case EResources.Stone:
                    {
                        gs.stone -= resource.amount;
                    }
                    break;

                case EResources.Bone:
                    {
                        gs.bone -= resource.amount;
                    }
                    break;

                case EResources.Iron:
                    {
                        gs.iron -= resource.amount;
                    }
                    break;
            }
            gs.UpdateResourses() ;
        
    }

    
    #endregion
}
