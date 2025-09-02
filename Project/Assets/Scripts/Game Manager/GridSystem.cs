using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public static GameObject[,] tileArray;
    public Vector2[] markIsOccupiedManually;

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
        //to manuly mark few tiles as occupied
        if (markIsOccupiedManually != null)
        {
            foreach (Vector2 position in markIsOccupiedManually)
            {
                MarkIsOccupiedAt(position);
            }
        }
    }

    public void UpDateGrid()
    {
        if(gridIsVisible)
        {
            GetGrid();
        }
        else
        {
            CancelInvoke();
        }
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
        }  
        gridIsVisible = false;
    }

    //enable the grid visible to the player/ on camera
    public void GetGrid()
    {
        //if (!gridIsVisible)
        {
            for (int i = 0; i < tileArray.GetLength(0); i++)
            {

                for (int j = 0; j < tileArray.GetLength(1); j++)
                {
                    Vector3 screenPoint = Camera.main.WorldToViewportPoint(tileArray[i, j].transform.position);//store the transform of the camera
                    bool onScreen = screenPoint.x > -0.2 && screenPoint.x < 1.2 && screenPoint.y > -0.2 && screenPoint.y < 1.2;//cheak if the tile is in the view of the camera
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
            //gridIsVisible = true;
        }

    }

    //return the nearest tile to the position given while calling the function
    public GameObject GetNearestTile(Vector2 position)
    {
        GameObject nearestTile = null;//temprory variable to store the nearest tile
        float nearestDistence = float.MaxValue;//temprory variavle to store the distance of that tile
        foreach (GameObject tile in GridSystem.tileArray)//run for each tile in grid system
        {
            float distance = Vector2.Distance(tile.transform.position, position);//calculate the distance of the current tile from th position
            //store the values if the distance is less then the previos distances
            if (distance < nearestDistence)
            {
                nearestDistence = distance;
                nearestTile = tile;
            }
        }
        return nearestTile;//return the nearest tile
    }

    //to mark few tiles as occupied
    public void MarkIsOccupiedAt(Vector2 position)
    {
        GetNearestTile(position).GetComponent<Tile>().isOccupied=true;
    }
    


}
