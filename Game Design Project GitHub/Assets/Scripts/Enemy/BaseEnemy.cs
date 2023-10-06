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


    protected GameObject Player;
    protected bool isBuilding = false;
    protected GameObject theBuilding;//temprory store the building in contact
    protected Rigidbody2D rigidBody;
    protected float lastAttackTime;//temprory store the time last attacked
    

    //follow the target
    public void FollowTarget(Vector2 target , Vector2 selfPos)
    {
        Vector2 direction;//temprory store the direction
        if (Vector2.Distance(transform.position, target) > minimumDistanceFromPlayer)//cheack if the distance between you and the taeget is more then the minimumDistance distance
        {
            direction = target - selfPos;//set the direction from self Position to target position
        }
        else//if the enemy has reach the taeget
        {
            direction = Vector2.zero;//set direction to zero
        }
        rigidBody.velocity = (direction.normalized) * moveSpeed;//make it move in the direction of the enemy
    }

    //attack the building/Player
    protected void Attack(GameObject target , float damage)//get the target to attack and the damage to been delt
    {
        lastAttackTime = Time.time;//seting the time of the last attack
        Debug.Log(damage + " damage to " + target.name);
        if(isBuilding)//if it is a building 
        {
            BuildingBuleprint bp =  target.GetComponent<BuildingBuleprint>();//creating an instance of the class BuildingBlueprint
            bp.TakeDamage(damage);//calling the function takedamage from the building script
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
