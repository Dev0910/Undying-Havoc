using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public WeaponScriptableObjects[] weaponScriptableObjects; // Taking reference for the scriptable Objects
    public int currentWeaponscriptableobjectIndex = 0;
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
    private SpriteRenderer spriteRenderer;
    //public bool isBought;
    //public int currentLevel; // Index for weaponsData
    //public bool isBought;

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyToAttack = null;
        //isBought = false;
        weaponsData = weaponScriptableObjects[currentWeaponscriptableobjectIndex].weaponsData;
        currentLevel = 0;
        WeaponGetData();
        spriteRenderer.sprite = currentWeaponSpriite;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && enemyToAttack != null)
        {
            enemyToAttack.GetComponent<BaseEnemy>().TakeDamage(damage);
            //Debug.Log(enemyToAttack.name+" : " + damage);
        }
        // Mouse scroll wheel input
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel > 0)
        {
            // Scroll up, switch to the next weapon
            SwitchWeapon(1);
        }
        else if (scrollWheel < 0)
        {
            // Scroll down, switch to the previous weapon
            SwitchWeapon(-1);
        }
    }

    void SwitchWeapon(int offset)
    {
        int newWeaponscriptableobjectIndex = currentWeaponscriptableobjectIndex + offset;

        // Ensure the new index is within the array bounds
        if (newWeaponscriptableobjectIndex < 0)
            newWeaponscriptableobjectIndex = weaponScriptableObjects.Length - 1;
        else if (newWeaponscriptableobjectIndex >= weaponScriptableObjects.Length)
            newWeaponscriptableobjectIndex = 0;

        currentWeaponscriptableobjectIndex = newWeaponscriptableobjectIndex;
        weaponsData = weaponScriptableObjects[currentWeaponscriptableobjectIndex].weaponsData;
        WeaponGetData();
        spriteRenderer.sprite = currentWeaponSpriite;
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
        spriteRenderer.sprite = currentWeaponSpriite;
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