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
        target = GameManager.Instance.oxygenGenerator.gameObject;

        
        GetData();//reset values
        currentMoveSpeed = moveSpeed;
        
        healthbar.fillAmount = currentHealth / maxHealth;//update UI
    }

    private void Start()
    {
        StartCoroutine(AttackTarget());
    }
    private void Update()
    {
        FollowTarget(target.transform.position);//calling the follow function from the base class
    }

    //cheak for collision for Player or Buildings
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Building") || collision.gameObject.CompareTag("Player"))
        {
            targetToAttack = collision.gameObject;
        }
    }

    //cheak for exit from a collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(targetToAttack == null) 
        {
            return;
        }
        if(collision.gameObject.tag == targetToAttack.gameObject.tag)
        {
            targetToAttack = null;
        }
    }
    IEnumerator AttackTarget()
    {
        if (targetToAttack != null)
        {
            yield return new WaitForSeconds(attackAfterSecondsOfContact);
            //need to cheak it twice because the target to attack can leave the range in the it is waiting
            if(targetToAttack != null)
            {
                Attack(targetToAttack, currentDamage);//calling the function from the base class and giving the object to be attacked and the damage to be delt
            }
            
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }
        //loop it self
        StartCoroutine(AttackTarget());
    }

    //used in case the traps are used

    ////cheak for Traps
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Trap"))
    //    {
    //        trap=collision.gameObject;
    //        trapCount++;
    //    }
    //}
    ////cheak for exit from Traps
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Trap"))
    //    {
    //        trapCount--;
    //        if (trapCount <= 0)
    //        {
    //            trap = null;
    //        }
    //    }
    //}
}