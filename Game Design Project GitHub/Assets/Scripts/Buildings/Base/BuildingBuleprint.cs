using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingBuleprint : MonoBehaviour
{
    [Header("Unity Things")]
    public BuildingScriptableObjects buildingScriptableObjects;
    public Sprite upgradedSprite;

    [Header("Details")]
    public float currentHealth = 0f;
    public int currentLevel = 0;

    [Header("Money")]
    public int cost;
    public int sellPrice;
    public int upgradeCost;

    [Header("Attack")]
    public int damage;
    
    


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
        if(ClickHandler.vDown && GameStats.currentGold >= upgradeCost && upgradedSprite != null)
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
            this.gameObject.SetActive(false);
        }
    }

    void SellBuilding()
    {
        GameStats.currentGold += sellPrice;//adding the selling price of the turret 
        Destroy(this.gameObject);//destroying the building

        //change the isOccuied bool in the class Tile
        if (nearestTile != null && nearestTile.GetComponent<Tile>().isOccupied == true)
        {
            nearestTile.GetComponent<Tile>().isOccupied = false;
        }
    }
    void UpgradeBuilding()
    {
        GameStats.currentGold -= upgradeCost;//removing the upgrade cost
        currentLevel++;
        GetData();
        spriteRenderer.sprite = currentSpriite;
    }


    protected void GetData()
    {
        currentSpriite = buildingData[currentLevel].buildingSprite;
        currentBulletSprite = buildingData[currentLevel].bulletSprite;
        currentHealth = buildingData[currentLevel].health;
        cost = buildingData[currentLevel].cost;
        sellPrice = buildingData[currentLevel].sellingPrice;
        damage = buildingData[currentLevel].damage;
        if (currentLevel < buildingData.Length - 1)
        {
            upgradedSprite = buildingData[currentLevel + 1].buildingSprite;
            upgradeCost = buildingData[currentLevel + 1].cost;
        }
        else
        {
            upgradeCost = int.MaxValue;
        }
        
    }

}
