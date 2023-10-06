using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    private void Awake()
    {
        //GetEnemyData();
        currentHealth = maxHealth;
        isBuilding = false;
        rigidBody = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        FollowTarget(Player.transform.position , this.transform.position);//calling the follow function from the base class
        if (lastAttackTime + attackSpeed < Time.time && isBuilding)//cheak if it can attack the building
        {
            Attack(theBuilding,damage);//calling the function from the base class and giving the object to be attacked and the damage to be delt
        }
    }

    //cheak for collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if it is a building then set the isBuilding true and temprory store it in the building
        if(collision.gameObject.CompareTag("Building"))
        {
            isBuilding = true;
            theBuilding = collision.gameObject;
        }
    }

    //cheak for exit from a collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        //reset the temperory building values
        if(collision.gameObject.CompareTag("Building"))
        {
            isBuilding = false;
            theBuilding = null;
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
