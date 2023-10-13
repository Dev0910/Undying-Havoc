using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponScriptableObjects weaponScriptableObjects; // Taking reference for the scriptable Objects
    private WeaponData[] weaponsData;
    public float attackrange; // Range for the weapon
    public LayerMask enemylayers; // Enemy Layer for attacking the enemy
    public Sprite currentWeaponSpriite; // Base weapon Sprite
    public int costToBuy; // cost to buy the weapon
    public float damage; // Damage dealt by weapon
    public Sprite upgradedSprite; // New sprite for Upgraded Weapon
    public int costToUpgrade; // cost to upgrade the weapon
    public int currentLevel; // Index for weaponsData
    //public bool isBought;
    //public int currentLevel; // Index for weaponsData
    //public bool isBought;


     void Start()
    {
        //isBought = false;
        weaponsData = weaponScriptableObjects.weaponsData;
        //currentLevel = 0;
        WeaponGetData();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {
        /* Collider2D[] hitenemies = Physics2D.OverlapCircleAll(transform.position, attackrange, enemylayers); // collecting all the colliders in an array which have an enemy layer by creating an imaginary circle with radius attackrange

         foreach (Collider2D enemy in hitenemies)
         {
             Debug.Log("Weapon Damage : " + damage);
             enemy.GetComponent<Enemy1>().TakeDamage(damage); // Running through the loop and each time we get an collider in the array the enemy take damage
         }*/
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackrange, enemylayers);

            Debug.Log("Number of enemies hit: " + hitEnemies.Length);

            foreach (Collider2D enemy in hitEnemies)
            {
                BaseEnemy enemyScript = enemy.GetComponent<BaseEnemy>();
                if (enemyScript != null)
                {
                    Debug.Log("Weapon Damage: " + damage);
                    enemyScript.TakeDamage(damage);
                }
                else
                {
                    Debug.LogWarning("Enemy does not have the Enemy1 component.");
                }
            }
        

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackrange); // This function helps to make the imaginary circle visible
    }

    public void UpgradeWeapon()
    {
        GameStats.currentGold -= costToUpgrade;//removing the upgrade cost
        currentLevel++;
        WeaponGetData();
    }

    void WeaponGetData()
    {
        currentWeaponSpriite = weaponsData[currentLevel].weaponSprite;
        costToBuy = weaponsData[currentLevel].cost;
        damage = weaponsData[currentLevel].damage;

        if (currentLevel < weaponsData.Length - 1)
        {
            upgradedSprite = weaponsData[currentLevel + 1].weaponSprite;
            costToUpgrade = weaponsData[currentLevel + 1].cost;
        }
        else
        {
            costToUpgrade = int.MaxValue;
        }
    } 
}