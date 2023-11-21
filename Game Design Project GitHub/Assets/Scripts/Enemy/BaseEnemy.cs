using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
    [Header("Health")]
    public float startHealth;
    public float currentHealth;
    protected float maxHealth;
    public float incrementInHpEachWaveInPercentage;

    [Header("Gold")]
    public float startValueInGold;
    public int currentValueInGold;
    public float incrementInGoldValueInPersentage;
    protected float maxValueInGold;

    [Header("Movement")]
    public float moveSpeed;
    public float minimumDistanceFromPlayer;

    [Header("Attack")]
    public float attackSpeed;
    public float startDamage;
    public float currentDamage;
    protected float maxDamage;
    public float incrementInDamageEachWaveInPercentage;
    public float attackAfterSecondsOfContact;

    [Header("UI")]
    public Image healthbar;

    [Header("Particle Effect")]
    public GameObject Blood;

    //used for Traps
    protected GameObject trap;
    protected int trapCount;//to store the number of trap the enemy is on
    protected float trapTimer;


    protected float currentMoveSpeed;//update the speed in game
    protected GameObject player;//instance of player
    protected int collisionCount;//to count the collision
    public GameObject targetToAttack;//temprory store the building in contact
    protected Rigidbody2D rigidBody;
    protected float lastAttackTime;//temprory store the time last attacked
    protected float timeFromContact;//to count the time from the time plyer made contact
    
    
    

    //follow the target
    public void FollowTarget(Vector2 target)
    {
        Vector2 selfPosition = transform.position;
        Vector2 direction;//temprory store the direction
        if (Vector2.Distance(selfPosition, target) > minimumDistanceFromPlayer)//cheack if the distance between you and the taeget is more then the minimumDistance distance
        {
            direction = target - selfPosition;//set the direction from self Position to target position
        }
        else//if the enemy has reach the taeget
        {
            direction = Vector2.zero;//set direction to zero
        }
        rigidBody.velocity = (direction.normalized) * currentMoveSpeed;//make it move in the direction of the enemy
        LookAtPlayer();
    }

    //look at player
    private void LookAtPlayer()
    {
        Vector2 lookDir = new Vector2(player.transform.position.x,player.transform.position.y) - rigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rigidBody.rotation = angle;
    }

    //attack the building/Player
    protected void Attack(GameObject target , float damage)//get the target to attack and the damage to been delt
    {
        lastAttackTime = Time.time;//seting the time of the last attack
        

        if(target.gameObject.CompareTag("Building"))//if it is a building 
        {
            BuildingBuleprint bp =  target.GetComponent<BuildingBuleprint>();//creating an instance of the class BuildingBlueprint
            bp.TakeDamage(damage);//calling the function takedamage from the building script
            //Debug.Log(damage + " damage to " + target.name);
        }
        else if(target.gameObject.CompareTag("Player"))
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            pc.TakeDamage(damage);
        }
        
    }

    //take damage from buildings/Player
    public void TakeDamage(float damage)
    {
        if (currentHealth  > 0)
        {
            currentHealth -= damage;
        }
        if(currentHealth <= 0)
        {
            GameManager.Instance.dropAndCollectionManager.DropGold(this.transform.position,currentValueInGold);
            GameStats.score++;
            Destroy(this.gameObject);
            GameObject bloodEffect = Instantiate(Blood, transform.position, Quaternion.identity);
            GameObject.Destroy(bloodEffect,0.5f);
        }
        healthbar.fillAmount = currentHealth / maxHealth;
    }

    protected void ChangeMoveSpeed(float changeInMoveSpeed)
    {
        currentMoveSpeed = moveSpeed * changeInMoveSpeed;
    }
    protected void DefaultMoveSpeed()
    {
        currentMoveSpeed = moveSpeed;
    }

    //All the things to don when steped on a trap
    protected void Trap()
    {
        //if stepping on Trap
        if (trapCount > 0)
        {
            TrapBlueprint trapBlueprint = trap.GetComponent<TrapBlueprint>();//set Instance
            switch (trapBlueprint.trapType)
            {
                case EType.None: break;

                case EType.SlowTrap:
                    {
                        ChangeMoveSpeed(trapBlueprint.enemySpeed);//slow the enemy
                        break;
                    }

                case EType.MagmaTrap:
                    {
                        ChangeMoveSpeed(trapBlueprint.enemySpeed);//slow the enemy
                        //damage player according to the attack rate 
                        if (trapTimer <= 0)
                        {
                            trapTimer = 1f / trapBlueprint.attackRate;
                            TakeDamage(trapBlueprint.damage);

                        }
                        trapTimer -= Time.deltaTime;
                        break;
                    }
            }
        }

        //reset the enemy movement speed if not steping on a trap
        else
        {
            DefaultMoveSpeed();
        }
    }

    protected void GetData()
    {
        maxHealth = startHealth;
        maxDamage = startDamage;
        maxValueInGold = startValueInGold;

        for(int i = 0;i < SpawnMannager.currentWave;i++)
        {
            maxHealth += (maxHealth * (incrementInHpEachWaveInPercentage / 100));
            maxDamage += (maxDamage * (incrementInDamageEachWaveInPercentage / 100));
            maxValueInGold += (maxValueInGold * (incrementInGoldValueInPersentage / 100));
            
        }
        currentHealth = Mathf.Round(maxHealth * 100.00f) * 0.01f;
        currentDamage = Mathf.Round(maxDamage * 100.00f) * 0.01f;
        currentValueInGold = Mathf.RoundToInt(maxValueInGold);
    }
}
