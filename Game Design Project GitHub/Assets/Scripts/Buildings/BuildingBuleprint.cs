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
        nearestTile = GameManager.gridSystem.GetNearestTile(this.transform.position);
    }
    private void OnMouseDown()
    {
        //to sell the building 
        if(ClickHandler.xDown)
        {
            Stats.currentGold += sellPrice;
            Destroy(this.gameObject);


            
            if (nearestTile != null && nearestTile.GetComponent<Tile>().isOccupied == true)
            {
                nearestTile.GetComponent<Tile>().isOccupied = false;
            }
        }
        //to upgrade the building
        if(ClickHandler.vDown && Stats.currentGold >= upgradeCost && upgradedPrefab != null)
        {
            Stats.currentGold -= upgradeCost;
            Instantiate(upgradedPrefab, this.transform.position , upgradedPrefab.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float Damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= Damage;
        }
        else if(currentHealth <= 0)
        {
            //currentHealth = 0;
            Debug.Log("Building Destroyed");
            nearestTile.GetComponent<Tile>().isOccupied = false;
            this.gameObject.SetActive(false);
        }
    }

}
