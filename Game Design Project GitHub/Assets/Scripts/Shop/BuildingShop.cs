using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShop : MonoBehaviour
{
    private GameObject buildingToPlace;
    public CustomCurser customCurser;
    

    // Update is called once per frame
    void Update()
    {
        //place the building on left mouse click
        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
        {
            //get the nearest tile by calling the get nearest tile function in grid system
            GameObject nearestTile = GameManager.Instance.gridSystem.GetNearestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //cheak the comditions to place a building
            if (nearestTile != null && nearestTile.GetComponent<Tile>().isOccupied == false && GameStats.currentGold >= buildingToPlace.GetComponent<BuildingBuleprint>().cost)
            {
                GameStats.currentGold -= buildingToPlace.GetComponent<BuildingBuleprint>().cost;//take the cost of building
                Instantiate(buildingToPlace, nearestTile.transform.position, Quaternion.identity);//spawn the building
                nearestTile.GetComponent<Tile>().isOccupied = true;//make the isOccupied tile true

                //if you don't have the mony to buy the next building
                if (GameStats.currentGold - buildingToPlace.GetComponent<BuildingBuleprint>().cost < 0)
                {
                    DeselectBuilding();
                }
            }

        }
        //deselect the Building on Right mouse click
        if (Input.GetMouseButtonDown(1))
        {
            DeselectBuilding();
        }
    }

    //set the building to be bought on button click
    //this function is called by the buttons in UI
    public void BuyBuilding(GameObject building)
    {
        //cheaks the conditions to buy the building
        if (GameStats.currentGold >= building.GetComponent<BuildingBuleprint>().cost)
        {
            customCurser.gameObject.SetActive(true);
            customCurser.GetComponent<SpriteRenderer>().sprite = building.GetComponent<BuildingBuleprint>().buildingScriptableObjects.buildingData[0].UI_Sprite;//sets the curser to the sprite of the building
            Cursor.visible = false;


            buildingToPlace = building;//set the buildingToPlace to this building
            GridSystem.gridIsVisible = true;
            GameManager.Instance.gridSystem.InvokeRepeating("UpDateGrid", 0, 1);
            //gameObject.GetComponent<GridSystem>().GetGrid();//draws the grid
        }
    }

    //Deselect the Building from the curser
    private void DeselectBuilding()
    {
        buildingToPlace = null;
        customCurser.gameObject.SetActive(false);
        Cursor.visible = true;
        GridSystem.gridIsVisible = false;
        //gameObject.GetComponent<GridSystem>().RemoveGrid();
    }
}
