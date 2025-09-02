using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public EnemyData[] enemyData;
}

[System.Serializable]
public class EnemyData
{
    public EEnemyType enemyType;
    public GameObject enemyPrefab;
    public int costToSpawn;
    public float maxHealth;
    public float moveSpeed;
    public float attackSpeed;
    public float damage;
    public float minimumDistanceFromPlayer;
}
    public enum EEnemyType
{
    none,
    Enemy1,
    Enemy2,
    Enemy3
}