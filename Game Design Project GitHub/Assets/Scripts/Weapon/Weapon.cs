using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public int damagetaken = 20;  // Damage of the weapon
    public float attackrange = 0.4f; // Range for the weapon
    public LayerMask enemylayers;
    public int costToBuy; // cost to buy the weapon
    public int costToUpgrade; // cost to upgrade the weapon

    public WeaponScriptableObjects weaponScriptableObjects;
    private WeaponData[] weaponsData;
    public Sprite currentWeaponSpriite;
    public int damage;
    public int cost;
    public Sprite upgradedSprite;
    public int upgradeCost;
    public int currentLevel = 0;

     void Start()
    {
        currentLevel = 0;
        WeaponGetData();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
            Debug.Log(damage);
        }
    }

    void Attack()
    {

        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(transform.position, attackrange, enemylayers); // collecting all the colliders in an array which have an enemy layer by creating an imaginary circle with radius attackrange

        foreach (Collider2D enemy in hitenemies)
        {
            enemy.GetComponent<Enemy1>().TakeDamage(damage); // Running through the loop and each time we get an collider in the array the enemy take damage
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackrange); // This function helps to make the imaginary circle visible
    }

    void WeaponGetData()
    {
        currentWeaponSpriite = weaponsData[currentLevel].weaponSprite;
        cost = weaponsData[currentLevel].cost;
        damage = weaponsData[currentLevel].damage;
    } 
}

