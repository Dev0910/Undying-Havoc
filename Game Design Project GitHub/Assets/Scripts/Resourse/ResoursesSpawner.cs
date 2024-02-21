using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResoursesSpawner : MonoBehaviour
{
    [SerializeField] private static List<GameObject> resourseList = new List<GameObject>();

    [SerializeField] private float startMinDistanceFromEachOrher = 10f;
    [SerializeField] private float respawnEvery = 30f;
    [SerializeField] private ResoursesList[] resoursesList;

    private Vector2 mapDimensions;
    float currentMinDistance;
    Vector2 spawnPos;
    //private float respawnTimmer;
    // Start is called before the first frame update
    void Start()
    {
        mapDimensions = GameObject.Find("Map").transform.lossyScale;
        mapDimensions -= new Vector2(10, 10);
        currentMinDistance = startMinDistanceFromEachOrher;
        //respawnTimmer = UnityEngine.Random.Range((respawnEvery - (respawnEvery/5)), (respawnEvery + respawnEvery/5));
        StartCoroutine(ReSpawnResourses());
        InitalSpawn();

    }

    //Re-Spawns Resourses
    IEnumerator ReSpawnResourses()
    {
        float waitForSec = UnityEngine.Random.Range((respawnEvery - (respawnEvery / 5)), (respawnEvery + respawnEvery / 5));
        yield return new WaitForSeconds(waitForSec);

        bool hasSpawn = false;
        while (!hasSpawn)
        {
            RandomSpawnPoints();

            if (FindShortestDistance(spawnPos) > currentMinDistance)
            {
                Instantiate(resoursesList[UnityEngine.Random.Range(0, resoursesList.Length)].resoursePrefab, spawnPos, Quaternion.identity).transform.parent = GameObject.Find("ResourcesHolder").transform;
                hasSpawn = true;
            }
            else
            {
                currentMinDistance -= currentMinDistance > 0 ? 0.001f : 0;
            }
        }

        StartCoroutine(ReSpawnResourses());//calles itself to make it infinite
    }

    //Called at the start of the game to spawn the firts few resourses;
    void InitalSpawn()
    {
        for(int i = 0; i < resoursesList.Length; i++)
        {

            RandomSpawnPoints();
            Instantiate(resoursesList[i].resoursePrefab, spawnPos, Quaternion.identity).transform.parent = GameObject.Find("ResourcesHolder").transform;

            for (int j = 1; j < resoursesList[i].numberOfSpawns; j++)
            {
                bool hasSpawn = false;
                while (!hasSpawn)
                {
                    RandomSpawnPoints();
                    
                    if (FindShortestDistance(spawnPos) > currentMinDistance)
                    {
                        Instantiate(resoursesList[i].resoursePrefab, spawnPos, Quaternion.identity).transform.parent = GameObject.Find("ResourcesHolder").transform;
                        hasSpawn = true;
                    }
                    else
                    {
                        currentMinDistance -= currentMinDistance > 0 ? 0.001f : 0;
                    }
                }
                //yield return null;
            }
        }
    }
    //Resets the spawn points to a Random 
    private void RandomSpawnPoints()
    {
        spawnPos.x = UnityEngine.Random.Range(-(mapDimensions.x / 2), (mapDimensions.x / 2));
        spawnPos.y = UnityEngine.Random.Range(-(mapDimensions.y / 2), (mapDimensions.y / 2));
    }

    //Returns the shortest distance from the spawn point and other 
    private float FindShortestDistance(Vector2 spawnPos)
    {
        float shortestDistance = float.MaxValue;
        GameObject[] SourceList = GameObject.FindGameObjectsWithTag("Sourse");//store all the sources
        GameObject[] BuildingList = GameObject.FindGameObjectsWithTag("Building");//to store all the buildings

        //caluculate the shortest distance to a building
        foreach (GameObject target in SourceList)
        {
            float distance = Vector2.Distance(spawnPos, target.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
            }
        }
        foreach (GameObject target in BuildingList)
        {
            float distance = Vector2.Distance(spawnPos, target.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
            }
        }
        return shortestDistance;
    }
}

[Serializable]
public class ResoursesList
{
    public Esource name;
    //public static List<GameObject> spawnedResourses = new List<GameObject>();
    public GameObject resoursePrefab;
    public int numberOfSpawns;
}
