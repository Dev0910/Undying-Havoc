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
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI sellingPriceText;
    public TextMeshProUGUI upgradePriceText;

    [Header("Unity Things")]
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
    public List<SingleResourse> resourseList = new List<SingleResourse>();
    public int sellAmount;

    [Header("Attack")]
    public int damage;


    [Header("Particle Effect")]
    public GameObject destructionEffect;

    protected GameObject nearestTile = null;
    protected SpriteRenderer spriteRenderer;
    protected BuildingData[] buildingData;
    protected Sprite currentSpriite;
    protected Sprite currentBulletSprite;


    private void OnMouseDown()
    {
        //to sell the building 
        if(ClickHandler.xDown)//taking refrence from the click Handler
        {
            SellBuilding();
        }

        //to upgrade the building
        if(ClickHandler.vDown && CheakIfResourseAvailable(currentLevel+1) && upgradedSprite != null)
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
        //cost = buildingData[currentLevel].cost;
        //sellPrice = buildingData[currentLevel].sellingPrice;
        damage = buildingData[currentLevel].damage;
        if (currentLevel < buildingData.Length - 1)
        {
            upgradedSprite = buildingData[currentLevel + 1].buildingSprite;
            //upgradeCost = buildingData[currentLevel + 1].cost;
            //upgradePriceText.text = "Upgrade Price : " + upgradeCost;
        }
        else
        {
            //upgradeCost = int.MaxValue;
            upgradePriceText.text = "Max Level";
        }

        //hover UI data
        damageText.text = "Damage : " + damage;
        //sellingPriceText.text = "Selling Price : " + sellPrice;
        

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
        foreach (SingleResourse resourse in buildingData[currentLevel].resourseList)
        {
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
                        gs.stone += resourse.amount;
                    }
                    break;

                case EResources.Bone:
                    {
                        gs.bone += resourse.amount;
                    }
                    break;

                case EResources.Iron:
                    {
                        gs.iron += resourse.amount;
                    }
                    break;
            }
        }
    }
#endregion
}
