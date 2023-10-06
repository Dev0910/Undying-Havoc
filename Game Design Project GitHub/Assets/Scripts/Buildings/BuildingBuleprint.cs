using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingBuleprint : MonoBehaviour
{
    [Header("Building Blueprint")]
    public int cost;
    public int sellPrice;
    public GameObject upgradedPrefab;
    public int upgradeCost;
    public Sprite spriteTOBuild;
    public float maxHealth = 100f;
    public float currentHealth = 0f;
    GameObject nearestTile = null;

    private void Start()
    {
        currentHealth = maxHealth;
        nearestTile = GameManager.Instance.gridSystem.GetNearestTile(this.transform.position);//geting the nearest tile by calling the function in class GridSystem
    }
    private void OnMouseDown()
    {
        //to sell the building 
        if(ClickHandler.xDown)//taking refrence from the click Handler
        {
            GameStats.currentGold += sellPrice;//adding the selling price of the turret 
            Destroy(this.gameObject);//destroying the building

            //change the is Occuied bool in the class Tile
            if (nearestTile != null && nearestTile.GetComponent<Tile>().isOccupied == true)
            {
                nearestTile.GetComponent<Tile>().isOccupied = false;
            }
        }

        //to upgrade the building
        if(ClickHandler.vDown && GameStats.currentGold >= upgradeCost && upgradedPrefab != null)
        {
            GameStats.currentGold -= upgradeCost;//removing the upgrade cost
            Instantiate(upgradedPrefab, this.transform.position , upgradedPrefab.transform.rotation);//spawning the new building
            Destroy(this.gameObject);//destroying old building
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

}
