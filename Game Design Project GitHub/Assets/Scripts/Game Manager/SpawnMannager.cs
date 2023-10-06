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

    private int waveValue;
    public int incrementInWaveValu = 10;//the Valu of wave increased each round
    public List<GameObject> enemiesToSpawn = new List<GameObject>();//to store the enemy to spawn each round

    private float waveDuration;//time between to waves
    public float spawnEnemySecondsBeforeDay;
    public float spawnRadius = 10f;
    private float spawnInterval;
    private int currentWave;
    private GameObject parentGameObject;
    private int enemySpawnIndex = 0;
    //public List<GameObject> spawnedEnemies = new List<GameObject>();

    private GameObject player;
    private void Start()
    {
        currentWave = 1;
        parentGameObject = GameObject.FindGameObjectWithTag("EnemyHolder");
        player = GameManager.Instance.player;
        waveDuration = GameManager.Instance.dayAndNight.timeBetweenDayAndNight;
    }

    //start of the wave
    //called by the GameManager
    public void SpawnWave()
    {
        GetEnemysToSpawn();//update the enemiesToSpawn List

        spawnInterval = (waveDuration - spawnEnemySecondsBeforeDay)/ enemiesToSpawn.Count;

        currentWave++;
        
        InvokeRepeating("SpawnEnemy", 0, spawnInterval);
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
        enemiesToSpawn = generatedEnemies;//passing the temp values to enemiesToSpawn
    }

    private void SpawnEnemy()
    {
        GameObject enemy = enemiesToSpawn[enemySpawnIndex];//temprory store the enemy to spawn
        float randomAngle = Random.Range(0f, 360f);//chose a random Angle at which the enemy will be spawned
        Vector2 spawnPosition = player.transform.position + (Quaternion.Euler(0, 0, randomAngle) * Vector2.right * spawnRadius);//I don't Know
        GameObject e = Instantiate(enemy, spawnPosition, Quaternion.identity);//spawn the enemy
        e.transform.parent = parentGameObject.transform;
        if(enemySpawnIndex >= enemiesToSpawn.Count-1)
        {
            enemySpawnIndex = 0;//reset index
            CancelInvoke();//stop Invoke
        }
        else
        {
            enemySpawnIndex++;//increase the spawn index
        }
    }
    
}