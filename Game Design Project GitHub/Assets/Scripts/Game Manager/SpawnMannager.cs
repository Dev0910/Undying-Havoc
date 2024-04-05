using CustomPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StoreEnemy
{
    public Pool enemyPool;
    public EEnemy enemyType;
    public int cost;
}
public class SpawnMannager : MonoBehaviour
{
    
    public List<StoreEnemy> enemies = new List<StoreEnemy>();//To get the list of enemy from inspecter

    public int waveValue;
    public int incrementInWaveValu = 10;//the Valu of wave increased each round
    public int maxWaveValue;
    public List<StoreEnemy> enemiesToSpawn = new List<StoreEnemy>();//to store the enemy to spawn each round

    private float waveDuration;//time between to waves
    public float spawnEnemySecondsBeforeDay;
    public float spawnRadius = 10f;
    private float spawnInterval;
    public static int currentWave;
    private int enemySpawnIndex = 0;
    //public List<GameObject> spawnedEnemies = new List<GameObject>();

    private GameObject player;
    private void Start()
    {
        currentWave = 1;
        waveValue = 0;
        player = GameManager.Instance.player;
        waveDuration = GameManager.Instance.dayAndNight.timeBetweenDayAndNight;
        foreach (StoreEnemy enemy in enemies)
        {
            enemy.enemyPool.InitalSpawn(this.transform);
        }
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
        waveValue = incrementInWaveValu * currentWave <= maxWaveValue ? incrementInWaveValu * currentWave : maxWaveValue;//increase the wave Value each wave

        List<StoreEnemy> generatedEnemies = new List<StoreEnemy>();//temp list to store enemy
        //run the loop until waveValue 0
        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);//to get Random Index of Enemy
            int randEnemyCost = enemies[randEnemyId].cost;//to temp store cost of the enemy to spawn 

            if (waveValue - randEnemyCost >= 0)//cheak if we the the value to get the enemy
            {
                generatedEnemies.Add(enemies[randEnemyId]);//add the enemy to the temp list
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
        //GameObject enemy = PoolOperator.TakeFromList(enemiesToSpawn[enemySpawnIndex].enemyPool);//temprory store the enemy to spawn
        GameObject enemy = enemiesToSpawn[enemySpawnIndex].enemyPool.TakeFromList();
        float randomAngle = Random.Range(0f, 360f);//chose a random Angle at which the enemy will be spawned
        Vector2 spawnPosition = player.transform.position + (Quaternion.Euler(0, 0, randomAngle) * Vector2.right * spawnRadius);//I don't Know
        //GameObject e = Instantiate(enemy, spawnPosition, Quaternion.identity);//spawn the enemy
        enemy.transform.position = spawnPosition;
        //e.transform.parent = GameObject.Find("EnemyHolder").transform;
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
    public void AddToList(GameObject _enemy , EEnemy _enemyType)
    {
        foreach(StoreEnemy enemy in enemies)
        {
            if(enemy.enemyType == _enemyType)
            {
                //PoolOperator.AddToList(_enemy,enemy.enemyPool);
                enemy.enemyPool.AddToList(_enemy);
            }
        }
    }
    
}