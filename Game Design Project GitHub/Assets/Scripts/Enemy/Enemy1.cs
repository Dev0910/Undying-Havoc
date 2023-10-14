using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    
    private void Awake()
    {
        //GetEnemyData();
        collisionCount = 0;
        currentHealth = maxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.player;
        currentMoveSpeed = moveSpeed;
        healthbar.fillAmount = currentHealth / maxHealth;
    }
    private void Update()
    {
        FollowTarget(player.transform.position);//calling the follow function from the base class
        if (lastAttackTime + attackSpeed < Time.time && targetToAttack != null)//cheak if it can attack the building
        {
            Attack(targetToAttack,damage);//calling the function from the base class and giving the object to be attacked and the damage to be delt
        }
    }

    //cheak for collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        targetToAttack = collision.gameObject;
        collisionCount++;
    }

    //cheak for exit from a collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionCount--;
        if(collisionCount <= 0)
        {
            targetToAttack = null;
        }
    }

    //it is to get data from the scriptable object of this enemy
    private void GetEnemyData()//not currently used
    {
        EnemyData[] enemyDataArray = GameManager.Instance.enemyDatas;

        foreach (EnemyData enemy in enemyDataArray)
        {
            if (enemy != null && enemy.enemyType == EEnemyType.Enemy1)
            {
                maxHealth = enemy.maxHealth;
                moveSpeed = enemy.moveSpeed;
                attackSpeed = enemy.attackSpeed;
                damage = enemy.damage;
                minimumDistanceFromPlayer = enemy.minimumDistanceFromPlayer;
                break;
            }
        }
    }

    
    
}