using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShop : MonoBehaviour
{
    private GameObject buildingToPlace;
    public CustomCurser customCurser;
    public SingleResourse[] buildingList;
    //private void Start()
    //{
    //    foreach(GameObject building in buildingsList)
    //    {
    //        Instantiate(building).SetActive(false);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        //place the building on left mouse click
        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
        {
            //get the nearest tile by calling the get nearest tile function in grid system
            GameObject nearestTile = GameManager.Instance.gridSystem.GetNearestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //cheak the comditions to place a building
            if (nearestTile != null && nearestTile.GetComponent<Tile>().isOccupied == false && buildingToPlace.GetComponent<BuildingBuleprint>().CheakIfResourseAvailable(0))
            {
                buildingToPlace.GetComponent<BuildingBuleprint>().RemoveResourse(0);//take the cost of building
                GameObject temp = Instantiate(buildingToPlace, nearestTile.transform.position, Quaternion.identity);//spawn the building
                temp.transform.parent = GameObject.Find("BuildingHolder").transform;
                nearestTile.GetComponent<Tile>().isOccupied = true;//make the isOccupied tile true

                //if you don't have the mony to buy the next building
                if (!buildingToPlace.GetComponent<BuildingBuleprint>().CheakIfResourseAvailable(0))
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
    public void BuyBuilding(int index)
    {
        GameObject building = buildingList[index].prefab;
        //cheaks the conditions to buy the building
        
        BuildingBuleprint bp = building.GetComponent<BuildingBuleprint>();
        if (CheakIfResourseAvailable(0))
        {
            customCurser.gameObject.SetActive(true);
                
            customCurser.GetComponent<SpriteRenderer>().sprite = bp.GetComponent<SpriteRenderer>().sprite;//sets the curser to the sprite of the building
            Cursor.visible = false;


            buildingToPlace = building;//set the buildingToPlace to this building
            GridSystem.gridIsVisible = true;
            GameManager.Instance.gridSystem.InvokeRepeating("UpDateGrid", 0, 0.5f);
            //gameObject.GetComponent<GridSystem>().GetGrid();//draws the grid
        }
    }

    //Deselect the Building from the curser
    private void DeselectBuilding()
    {
        buildingToPlace = null;
        customCurser.gameObject.SetActive(false);
        //customCurser.GetComponent<SpriteRenderer>().sprite = customCurser.defaultSprite;
        Cursor.visible = true;
        GameManager.Instance.gridSystem.RemoveGrid();

        //gameObject.GetComponent<GridSystem>().RemoveGrid();
    }
    public bool CheakIfResourseAvailable(int index)
    {
        GameStats gs = GameManager.Instance.gameStats;
        bool result = false;

        //foreach (SingleResourse resourse in buildingList)
        {
            switch (buildingList[index].resource)
            {
                case EResources.None: break;

                case EResources.Wood:
                    {
                        result = gs.wood >= buildingList[index].amount;
                    }
                    break;

                case EResources.Stone:
                    {
                        result = gs.stone >= buildingList[index].amount;
                    }
                    break;

                case EResources.Bone:
                    {
                        result = gs.bone >= buildingList[index].amount;
                    }
                    break;

                case EResources.Iron:
                    {
                        result = gs.iron >= buildingList[index].amount;
                    }
                    break;
            }
        }
        return result;
    }
}
