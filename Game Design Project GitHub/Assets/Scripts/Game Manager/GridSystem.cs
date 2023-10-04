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

    private int[,] gridArray;
    public static bool gridIsVisible;
    private Vector2 spawnPoint = new Vector2(-99.5f,-99.5f);
    private void Awake()
    { 
        tileArray = new GameObject[width,height];
        gridArray = new int[width,height];

        for(int i = 0; i < gridArray.GetLength(0); i++)
        {
            
            for(int j = 0; j < gridArray.GetLength(1); j++)
            {
                GameObject tile =  Instantiate(tilePrefab, spawnPoint, tilePrefab.transform.rotation);
                tile.transform.parent = GameObject.Find("Grid").transform;
                tileArray[i,j] = tile;
                tile.SetActive(false);
                gridIsVisible = false;
                spawnPoint.x += 1;
            }
            spawnPoint.x = -99.5f;
            spawnPoint.y += 1;
        }
        spawnPoint = new Vector3(-99.5f, -99.5f);
    }

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
    public void GetGrid()
    {
        if (!gridIsVisible)
        {
            for (int i = 0; i < gridArray.GetLength(0); i++)
            {

                for (int j = 0; j < gridArray.GetLength(1); j++)
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
