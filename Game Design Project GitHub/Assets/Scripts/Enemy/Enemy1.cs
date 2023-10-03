using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    private void Awake()
    {
        isBuilding = false;
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        FollowTarget(Player.transform.position , this.transform.position);
        if (lastAttackTime + attackSpeed < Time.time && isBuilding)
        {
            Attack(theBuilding,damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Building"))
        {
            isBuilding = true;
            theBuilding = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Building"))
        {
            isBuilding = false;
            theBuilding = null;
        }
    }

    

    
}
