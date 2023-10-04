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
    
    public List<Enemy> enemies = new List<Enemy>();//To get the list of enemy from inspecter

    //private EnemyData[] enemies;
    //public int currWave;

    private int waveValue;
    public int incrementInWaveValu = 10;//the Valu of wave increased each round
    public List<GameObject> enemiesToSpawn = new List<GameObject>();//to store the enemy to spawn each round

    //public Transform[] spawnLocation;
    //public int spawnIndex;

    
    public float waveDuration;//time between to waves
    public float spawnEnemySecBeforeDay;
    public float spawnRadius = 10f;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    private int currentWave;
    //public List<GameObject> spawnedEnemies = new List<GameObject>();

    private GameManager gameManager;
    private GameObject player;
    private void Start()
    {
        currentWave = 1;
        //enemies = GameManager.enemyDatas;
        gameManager = GetComponent<GameManager>();
        player = gameManager.player;
        waveDuration = gameManager.timeBetweenDayAndNight;
        //InvokeRepeating("SpawnWave", waveDuration, waveDuration*2);
    }

    public void SpawnWave()
    {
        GetEnemysToSpawn();//update the enemiesToSpawn List

        spawnInterval = (waveDuration - spawnEnemySecBeforeDay)/ enemiesToSpawn.Count;

        //InvokeRepeating("SpawnEnemy", 0, spawnInterval);
        currentWave++;
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            SpawnEnemy(enemiesToSpawn[i]);
        }
    }


    //Randomly choose enemy to spawn accordind to the wave value
    private void GetEnemysToSpawn()
    {
        waveValue = incrementInWaveValu * currentWave;//increase the wave Value each wave

        List<GameObject> generatedEnemies = new List<GameObject>();//temp list to store enemy
        //run the loop until waveValue 0
        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);//to get Random Index of Enemy
            int randEnemyCost = enemies[randEnemyId].cost;//to temp store cost of the enemy to spawn 

            if (waveValue - randEnemyCost >= 0)//cheak if we the the value to get the enemy
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);//add the enemy to the temp list
                waveValue -= randEnemyCost;//remove the value of the enmey from the total value
            }
            else if (waveValue <= 0)//break the loop if the wave Valu is over
            {
                break;
            }
        }
        enemiesToSpawn.Clear();//empty the list 
        enemiesToSpawn = generatedEnemies;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        float randomAngle = Random.Range(0f, 360f);
        Vector2 spawnPosition = transform.position + Quaternion.Euler(0, 0, randomAngle) * Vector2.right * spawnRadius;
        Instantiate(enemy, spawnPosition, Quaternion.identity);

    }
    
}