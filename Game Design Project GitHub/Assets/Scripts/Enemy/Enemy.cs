using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseEnemy
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
        trapTimer = 0;
    }
    private void Update()
    {
        FollowTarget(player.transform.position);//calling the follow function from the base class
        if ((lastAttackTime + attackSpeed < Time.time) && (targetToAttack != null) && (timeFromContact + attackAfterSecondsOfContact < Time.time))//cheak if it can attack the building
        {
            Attack(targetToAttack, damage);//calling the function from the base class and giving the object to be attacked and the damage to be delt
        }
        Trap();
    }

    //cheak for collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        timeFromContact = Time.time;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            trap=collision.gameObject;
            trapCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            trapCount--;
            if (trapCount <= 0)
            {
                trap = null;
            }
        }
    }

    //it is to get data from the scriptable object of this enemy
    //private void GetEnemyData()//not currently used
    //{
    //    EnemyData[] enemyDataArray = GameManager.Instance.enemyDatas;

    //    foreach (EnemyData enemy in enemyDataArray)
    //    {
    //        if (enemy != null && enemy.enemyType == EEnemyType.Enemy1)
    //        {
    //            maxHealth = enemy.maxHealth;
    //            moveSpeed = enemy.moveSpeed;
    //            attackSpeed = enemy.attackSpeed;
    //            damage = enemy.damage;
    //            minimumDistanceFromPlayer = enemy.minimumDistanceFromPlayer;
    //            break;
    //        }
    //    }
    //}



}