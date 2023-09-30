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

    public void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void TakeDamage(float damage)
    {

    }

    public void FollowTarget(Vector2 target)
    {
        if (Vector2.Distance(transform.position,target) > minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }
}
