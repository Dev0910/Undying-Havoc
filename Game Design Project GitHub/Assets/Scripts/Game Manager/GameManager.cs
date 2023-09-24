using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    

    

    private GameObject buildingToPlace;
    public GameObject player;
    public static GameManager instance;
    
    public CustomCurser customCurser;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void Update()
    {
        

        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
        {
            GameObject nearestTile = null;
            float nearestDistence = float.MaxValue;
            foreach(GameObject tile in GridSystem.tileArray)
            {
                float dis = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (dis < nearestDistence)
                {
                    nearestDistence = dis;
                    nearestTile = tile;
                }
            }
            if (nearestTile != null && nearestTile.GetComponent<Tile>().isOccupied == false && Stats.currentGold >= buildingToPlace.GetComponent<BuildingBuleprint>().cost)
            {
                Stats.currentGold -= buildingToPlace.GetComponent<BuildingBuleprint>().cost;
                Instantiate(buildingToPlace, nearestTile.transform.position, Quaternion.identity);
                //buildingToPlace = null;
                nearestTile.GetComponent<Tile>().isOccupied = true;
                //gameObject.GetComponent<GridSystem>().RemoveGrid();
                //customCurser.gameObject.SetActive(false);
                //Cursor.visible = true;
                
            }
            
        }
        if(Input.GetMouseButtonDown(1))
        {
            buildingToPlace = null;
            customCurser.gameObject.SetActive(false);
            Cursor.visible = true;
            gameObject.GetComponent<GridSystem>().RemoveGrid();
        }
        
    }

    public void BuyBuilding(GameObject building)
    {
        if(Stats.currentGold >=building.GetComponent<BuildingBuleprint>().cost)
        {
            customCurser.gameObject.SetActive(true);
            customCurser.GetComponent<SpriteRenderer>().sprite = building.GetComponent<BuildingBuleprint>().spriteTOBuild;
            Cursor.visible = false;

            
            buildingToPlace = building;
            gameObject.GetComponent<GridSystem>().GetGrid();
        }
    }

    public void BuyWeapon(GameObject prefab)
    {
        if(prefab.GetComponent<PlayerCombat>().costToBuy <= Stats.currentGold) 
        {
            Stats.currentGold -= prefab.GetComponent<PlayerCombat>().costToBuy;
            GameObject instantiatedPrefab = Instantiate(prefab, player.transform.position, player.transform.rotation);
            instantiatedPrefab.transform.parent = player.transform;
        }
        
    }

}
