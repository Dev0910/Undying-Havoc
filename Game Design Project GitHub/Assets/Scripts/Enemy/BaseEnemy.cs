using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
    public float attackSpeed;
    public float damage;
    public float minimumDistanceFromPlayer;
    public int valueInGold;


    protected GameObject player;
    protected int collisionCount;//to count the collision
    protected GameObject targetToAttack;//temprory store the building in contact
    protected Rigidbody2D rigidBody;
    protected float lastAttackTime;//temprory store the time last attacked
    

    

    //attack the building/Player
    protected void Attack(GameObject target , float damage)//get the target to attack and the damage to been delt
    {
        lastAttackTime = Time.time;//seting the time of the last attack
        Debug.Log(damage + " damage to " + target.name);

        if(target.gameObject.CompareTag("Building"))//if it is a building 
        {
            BuildingBuleprint bp =  target.GetComponent<BuildingBuleprint>();//creating an instance of the class BuildingBlueprint
            bp.TakeDamage(damage);//calling the function takedamage from the building script
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
        if(currentHealth - damage >=0)
        {
            currentHealth -= damage;
        }
        else if(currentHealth <= 0)
        {
            GameManager.Instance.dropAndCollectionManager.DropGold(this.transform.position,valueInGold);
            Destroy(this.gameObject);
        }
    }
}
