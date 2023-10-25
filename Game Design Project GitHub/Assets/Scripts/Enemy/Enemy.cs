using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseEnemy
{
    
    private void Awake()
    {
        //set Instance
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.player;

        //reset values
        GetData();
        collisionCount = 0;
        currentMoveSpeed = moveSpeed;
        trapTimer = 0;

        //update UI
        healthbar.fillAmount = currentHealth / maxHealth;
    }
    private void Update()
    {
        FollowTarget(player.transform.position);//calling the follow function from the base class
        if ((lastAttackTime + attackSpeed < Time.time) && (timeFromContact + attackAfterSecondsOfContact < Time.time))//cheak if it can attack the building
        {
            if (targetToAttack != null)
            {
                Attack(targetToAttack, currentDamage);//calling the function from the base class and giving the object to be attacked and the damage to be delt
            }
        }
        Trap();
    }

    //cheak for collision for Player or Buildings
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


    //cheak for Traps
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            trap=collision.gameObject;
            trapCount++;
        }
    }
    //cheak for exit from Traps
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
}