using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public WeaponScriptableObjects[] weaponScriptableObjects;
    private WeaponScriptableObjects currentWeapon;// Taking reference for the scriptable Objects
    //public int currentWeaponscriptableobjectIndex = 0;
    public float attackrange; // Range for the weapon
    public LayerMask enemylayers; // Enemy Layer for attacking the enemy
    public Sprite currentWeaponSpriite; // Base weapon Sprite
    public int costToBuy; // cost to buy the weapon
    public float damage; // Damage dealt by weapon
    public Sprite upgradedSprite; // New sprite for Upgraded Weapon
    public int costToUpgrade; // cost to upgrade the weapon
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
        currentWeapon = weaponScriptableObjects[0];
        GetWeaponData(currentWeapon.weaponsData);
        spriteRenderer.sprite = currentWeaponSpriite;

        for(int i=0;i<weaponScriptableObjects.Length;i++)
        {
            //weaponScriptableObjects[i].isBought = false;
            weaponScriptableObjects[i].currentLevel = 0;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && enemyToAttack != null)
        {
            enemyToAttack.GetComponent<BaseEnemy>().TakeDamage(damage);
            //Debug.Log(enemyToAttack.name+" : " + damage);
        }

        SwitchWeapon();
        // Mouse scroll wheel input
        //float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        //if (scrollWheel > 0)
        //{
        //    // Scroll up, switch to the next weapon
        //    SwitchWeapon(1);
        //}
        //else if (scrollWheel < 0)
        //{
        //    // Scroll down, switch to the previous weapon
        //    SwitchWeapon(-1);
        //}
    }

    void SwitchWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = weaponScriptableObjects[0];
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = weaponScriptableObjects[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = weaponScriptableObjects[2];
        }

        GetWeaponData(currentWeapon.weaponsData);
        spriteRenderer.sprite = currentWeaponSpriite;
        //int newWeaponscriptableobjectIndex = currentWeaponscriptableobjectIndex + offset;
        //while(newWeaponscriptableobjectIndex>=-weaponScriptableObjects.Length && newWeaponscriptableobjectIndex<=weaponScriptableObjects.Length)
        //{
        //    if (newWeaponscriptableobjectIndex < 0)
        //        newWeaponscriptableobjectIndex = weaponScriptableObjects.Length - newWeaponscriptableobjectIndex;

        //    else if (newWeaponscriptableobjectIndex >= weaponScriptableObjects.Length)
        //        newWeaponscriptableobjectIndex = 0;
        //    if (weaponScriptableObjects[newWeaponscriptableobjectIndex].isBought)
        //    {
        //        break;
        //    }
        //    else
        //    {
        //        newWeaponscriptableobjectIndex += offset;
        //    }
        //}

        //if(!(newWeaponscriptableobjectIndex > -weaponScriptableObjects.Length && newWeaponscriptableobjectIndex < weaponScriptableObjects.Length))
        //{
        //    newWeaponscriptableobjectIndex = currentWeaponscriptableobjectIndex;
        //}
        //// Ensure the new index is within the array bounds


        //currentWeaponscriptableobjectIndex = newWeaponscriptableobjectIndex;
        //weaponsData = weaponScriptableObjects[currentWeaponscriptableobjectIndex].weaponsData;
        //WeaponGetData();
        //spriteRenderer.sprite = currentWeaponSpriite;
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
        spriteRenderer.sprite = currentWeaponSpriite;
    }

    void GetWeaponData(WeaponData[] weaponsData)
    {
        currentWeaponSpriite = weaponsData[currentWeapon.currentLevel].weaponSprite;
        costToBuy = weaponsData[currentWeapon.currentLevel].cost;
        damage = weaponsData[currentWeapon.currentLevel].damage;
        if(currentWeapon.currentLevel < weaponsData.Length - 1)
        {
            upgradedSprite = weaponsData[currentWeapon.currentLevel].weaponSprite;
            costToUpgrade = weaponsData[currentWeapon.currentLevel].cost;
        }
        else
        {
            costToUpgrade = int.MaxValue;
        }
        
   }
}