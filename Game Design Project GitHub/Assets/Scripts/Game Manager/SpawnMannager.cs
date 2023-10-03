using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
public class SpawnMannager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();

    //private EnemyData[] enemies;
    //public int currWave;

    private int waveValue;
    public int incrementInWaveValu = 10;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    //public Transform[] spawnLocation;
    public int spawnIndex;

    
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    //public List<GameObject> spawnedEnemies = new List<GameObject>();

    //private GameManager gameManager;
    private void Start()
    {
        //enemies = GameManager.enemyDatas;
        //gameManager = GetComponent<GameManager>();
        //InvokeRepeating("SpawnWave", 0, gameManager.timeBetweenDayAndNight);
    }

    public void SpawnWave(int currentWave)
    {
        GetEnemysToSpawn(currentWave);
        for(int i = 0;i<enemiesToSpawn.Count;i++)
        {
            SpawnEnemy(enemiesToSpawn[i]);
        }
    }



    private void GetEnemysToSpawn(int currentWave)
    {
        waveValue = incrementInWaveValu * currentWave;

        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    private void SpawnEnemy(GameObject _enemy)
    {
        Debug.Log("Spawn Enemy");
    }
}