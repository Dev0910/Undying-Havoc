using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject Player;
    public float moveSpeed;
    public float attackSpeed;
    public float damage;
    public float minimumDistance;
    protected bool isBuilding;
    protected Rigidbody2D rb;
    public float obstacleDetectionDistance = 1f;
    public void TakeDamage(float damage)
    {

    }

    public void FollowTarget(Vector2 target , Vector2 selfPos)
    {
        Vector2 dir;
        if (Vector2.Distance(transform.position, target) > minimumDistance)
        {
            dir = target - selfPos;
        }
        else
        {
            dir = Vector2.zero;
        }
        rb.velocity = (dir.normalized) * moveSpeed;
    }
}
