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
            pool.InitalSpawn(this.transform);
        }
    }
    public void AddToPool(GameObject _gameobject,EPool poolType)
    {
        foreach(var pool in pools)
        {
            if(pool.name == poolType)
            {
                pool.AddToList(_gameobject);
            }
        }
    }
    public GameObject TakeFromPool(EPool poolType)
    {
        foreach (var pool in pools)
        {
            if (pool.name == poolType)
            {
                return pool.TakeFromList();
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