using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
    [Header("Details")]
    public float maxHealth;
    public float currentHealth;
    public int valueInGold;

    [Header("Movement")]
    public float moveSpeed;
    public float minimumDistanceFromPlayer;

    [Header("Attack")]
    public float attackSpeed;
    public float damage;
    public float attackAfterSecondsOfContact;

    [Header("UI")]
    public Image healthbar;



    protected float currentMoveSpeed;
    protected GameObject player;
    protected int collisionCount;//to count the collision
    protected GameObject targetToAttack;//temprory store the building in contact
    protected Rigidbody2D rigidBody;
    protected float lastAttackTime;//temprory store the time last attacked
    protected float timeFromContact;


    

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
    }

    //attack the building/Player
    protected void Attack(GameObject target , float damage)//get the target to attack and the damage to been delt
    {
        lastAttackTime = Time.time;//seting the time of the last attack
        

        if(target.gameObject.CompareTag("Building"))//if it is a building 
        {
            BuildingBuleprint bp =  target.GetComponent<BuildingBuleprint>();//creating an instance of the class BuildingBlueprint
            bp.TakeDamage(damage);//calling the function takedamage from the building script
            Debug.Log(damage + " damage to " + target.name);
        }
        else if(target.gameObject.CompareTag("Player"))
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            pc.TakeDamage(damage);
            Debug.Log(damage + " damage to " + target.name);
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
            GameManager.Instance.dropAndCollectionManager.DropGold(this.transform.position,valueInGold);
            Destroy(this.gameObject);
        }
        healthbar.fillAmount = currentHealth / maxHealth;
    }

    public void ChangeMoveSpeed(float changeInMoveSpeed)
    {
        currentMoveSpeed *= changeInMoveSpeed;
    }
    public void DefaultMoveSpeed()
    {
        currentMoveSpeed = moveSpeed;
    }

    protected void OnMagmaTrap(float damage)
    {

    }

}
