using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject Player;
    public float moveSpeed;
    public float attackSpeed;
    public float damage;
    public float minimumDistanceFromPlayer;
    protected bool isBuilding = false;
    protected GameObject theBuilding;
    protected Rigidbody2D rb;
    public float obstacleDetectionDistance = 1f;
    protected float lastAttackTime;
    public void TakeDamage(float damage)
    {

    }

    public void FollowTarget(Vector2 target , Vector2 selfPos)
    {
        Vector2 dir;
        if (Vector2.Distance(transform.position, target) > minimumDistanceFromPlayer)
        {
            dir = target - selfPos;
        }
        else
        {
            dir = Vector2.zero;
        }
        rb.velocity = (dir.normalized) * moveSpeed;
    }

    protected void Attack(GameObject target , float damage)
    {
        lastAttackTime = Time.time;
        Debug.Log(damage + " damage to " + target.name);
        if(isBuilding)
        {
            target.GetComponent<BuildingBuleprint>().TakeDamage(damage);
        }
        
    }
}
