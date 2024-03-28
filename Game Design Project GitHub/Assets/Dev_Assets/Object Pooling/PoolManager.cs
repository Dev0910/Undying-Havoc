using CustomPool;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    public List<Pool> pools = new List<Pool>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        foreach (var pool in pools)
        {
            PoolOperator.InitalSpawn(pool,this.transform);
        }
    }
    public void AddToPool(GameObject _gameobject,EPool poolType)
    {
        foreach(var pool in pools)
        {
            if(pool.name == poolType)
            {
                PoolOperator.AddToList(_gameobject,pool);
            }
        }
    }
    public GameObject TakeFromPool(EPool poolType)
    {
        foreach (var pool in pools)
        {
            if (pool.name == poolType)
            {
                return PoolOperator.TakeFromList(pool);
            }
        }
        return null;
    }
}

public enum EPool
{
    none,
    Resource,
    Bullet
}