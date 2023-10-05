using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public static GameObject[,] tileArray;

    public static bool gridIsVisible;
    private Vector2 spawnPoint = new Vector2(-99.5f,-99.5f);
    private void Awake()
    { 
        tileArray = new GameObject[width,height];//array to store the tiles 
        
        //runs the for loop width*height times
        //spawns tile for each element in tileArray
        for(int i = 0; i < tileArray.GetLength(0); i++)
        {
            for(int j = 0; j < tileArray.GetLength(1); j++)
            {
                GameObject tile =  Instantiate(tilePrefab, spawnPoint, tilePrefab.transform.rotation);//spawn tiles
                tile.transform.parent = GameObject.Find("Grid").transform;//set Grid as their parent gameobject
                tileArray[i,j] = tile;//store the tiles in the array
                tile.SetActive(false);//disable the tile
                spawnPoint.x += 1;//increase the spawn point x by one unit
            }
            spawnPoint.x = -99.5f;//reset the x value of the spawn point
            spawnPoint.y += 1;//increase the spawn point y by one
        }
        spawnPoint = new Vector3(-99.5f, -99.5f);// reset the spawnpont
        gridIsVisible = false;
    }

    //disable all the visable grid
    public void RemoveGrid()
    {
        if (gridIsVisible)
        {
            for (int i = 0; i < tileArray.GetLength(0); i++)
            {
                for (int j = 0; j < tileArray.GetLength(1); j++)
                {
                    tileArray[i, j].SetActive(false);
                }
            }
        gridIsVisible = false;
        }     
    }

    //enable the grid visible to the player/ on camera
    public void GetGrid()
    {
        if (!gridIsVisible)
        {
            for (int i = 0; i < tileArray.GetLength(0); i++)
            {

                for (int j = 0; j < tileArray.GetLength(1); j++)
                {
                    Vector3 screenPoint = Camera.main.WorldToViewportPoint(tileArray[i, j].transform.position);
                    bool onScreen = screenPoint.x > -.2 && screenPoint.x < 1.2 && screenPoint.y > -0.2 && screenPoint.y < 1.2;
                    if (onScreen)
                    {
                        tileArray[i, j].SetActive(true);
                    }
                    else
                    {
                        tileArray[i, j].SetActive(false);
                    }
                }
            }
            gridIsVisible = true;
        }

    }

    public GameObject GetNearestTile(Vector2 position)
    {
        GameObject nearestTile = null;
        float nearestDistence = float.MaxValue;
        foreach (GameObject tile in GridSystem.tileArray)
        {
            float dis = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (dis < nearestDistence)
            {
                nearestDistence = dis;
                nearestTile = tile;
            }
        }
        return nearestTile;
    }
    


}
