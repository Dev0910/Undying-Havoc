using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    private GameObject enemyToAttack;
    //public bool isBought;
    //public int currentLevel; // Index for weaponsData
    //public bool isBought;
    void Start()
    {
        enemyToAttack = null;
        //isBought = false;
        weaponsData = weaponScriptableObjects.weaponsData;
        currentLevel = 0;
        WeaponGetData();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && enemyToAttack != null)
        {
            enemyToAttack.GetComponent<BaseEnemy>().TakeDamage(damage);
            //Debug.Log(enemyToAttack.name+" : " + damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyToAttack = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyToAttack = null;
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