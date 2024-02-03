using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResoursesSpawner : MonoBehaviour
{
    public float startMinDistanceFromEachOrher = 10f;
    [SerializeField] private float respawnEvery = 30f;
    [SerializeField] private ResoursesList[] resoursesList;

    private Vector2 mapDimensions;
    float currentMinDistance;
    Vector2 spawnPos;
    private float respawnTimmer;
    // Start is called before the first frame update
    void Start()
    {
        mapDimensions = GameObject.Find("Map").transform.lossyScale;
        mapDimensions -= new Vector2(10, 10);
        InitalSpawn();
        currentMinDistance = startMinDistanceFromEachOrher;
        respawnTimmer = UnityEngine.Random.Range((respawnEvery - (respawnEvery/5)), (respawnEvery + respawnEvery/5));
    }

    // Update is called once per frame
    void Update()
    {
        if(respawnTimmer < 0)
        {
            bool hasSpawn = false;
            while (!hasSpawn)
            {
                RandomSpawnPoints();

                if (FindShortestDistance(spawnPos) > currentMinDistance)
                {
                    Instantiate(resoursesList[UnityEngine.Random.Range(0,resoursesList.Length)].resoursePrefab, spawnPos, Quaternion.identity).transform.parent = GameObject.Find("ResourcesHolder").transform;
                    hasSpawn = true;
                }
                else
                {
                    currentMinDistance -= currentMinDistance > 0 ? 0.001f : 0;
                }
            }
            respawnTimmer = UnityEngine.Random.Range((respawnEvery - (respawnEvery / 5)), (respawnEvery + respawnEvery / 5));
        }
        else
        {
            respawnTimmer -= Time.deltaTime;
        }
    }

    private void InitalSpawn()
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

            }
        }
    }

    private void RandomSpawnPoints()
    {
        spawnPos.x = UnityEngine.Random.Range(-(mapDimensions.x / 2), (mapDimensions.x / 2));
        spawnPos.y = UnityEngine.Random.Range(-(mapDimensions.y / 2), (mapDimensions.y / 2));
    }
    private float FindShortestDistance(Vector2 spawnPos)
    {
        float shortestDistance = float.MaxValue;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Resources");


        foreach (GameObject target in targets)
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
    public GameObject resoursePrefab;
    public int numberOfSpawns;
}
