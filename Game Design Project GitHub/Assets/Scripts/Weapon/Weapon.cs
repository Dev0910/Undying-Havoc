using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public WeaponScriptableObjects[] weaponScriptableObjects;
    public float attackrange; // Range for the weapon
    public LayerMask enemylayers; // Enemy Layer for attacking the enemy
    public Sprite currentWeaponSpriite; // Base weapon Sprite
    public int costToBuy; // cost to buy the weapon
    public float damage; // Damage dealt by weapon
    public Sprite upgradedSprite; // New sprite for Upgraded Weapon
    public int costToUpgrade; // cost to upgrade the weapon
    
    private WeaponScriptableObjects currentWeapon;// Taking reference for the scriptable Objects
    private GameObject enemyToAttack;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        enemyToAttack = null;
        currentWeapon = null;
        boxCollider2D.enabled = false;
        spriteRenderer.sprite = null;

        for(int i=0;i<weaponScriptableObjects.Length;i++)
        {
            weaponScriptableObjects[i].isBought = false;
            weaponScriptableObjects[i].currentLevel = 0;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && enemyToAttack != null)
        {
            enemyToAttack.GetComponent<BaseEnemy>().TakeDamage(damage);
        }
        SwitchWeapon();
    }

    void SwitchWeapon()
    {
        if(currentWeapon == null) { return; }


        if(Input.GetKeyDown(KeyCode.Alpha1) && weaponScriptableObjects[0].isBought)
        {
            currentWeapon = weaponScriptableObjects[0];

        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && weaponScriptableObjects[1].isBought)
        {
            currentWeapon = weaponScriptableObjects[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weaponScriptableObjects[2].isBought)
        {
            currentWeapon = weaponScriptableObjects[2];
        }
        GetWeaponData(currentWeapon.weaponsData);
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

    public void UpgradeWeapon(int index)
    {
        if (weaponScriptableObjects[index].currentLevel < weaponScriptableObjects[index].weaponsData.Length)
        {
            GameStats.currentGold -= costToUpgrade;//removing the upgrade cost
            weaponScriptableObjects[index].currentLevel++;
        }
        
    }

    void GetWeaponData(WeaponData[] weaponsData)
    {
        currentWeaponSpriite = weaponsData[currentWeapon.currentLevel].weaponSprite;
        costToBuy = weaponsData[currentWeapon.currentLevel].cost;
        damage = weaponsData[currentWeapon.currentLevel].damage;
        if(currentWeapon.currentLevel < weaponsData.Length - 1)
        {
            upgradedSprite = weaponsData[currentWeapon.currentLevel].weaponSprite;
        }

        transform.localPosition = currentWeapon.weaponPosition;
        boxCollider2D.enabled = true;
        boxCollider2D.offset = currentWeapon.colliderOffSet;
        boxCollider2D.size = currentWeapon.colliderScale;
        spriteRenderer.sprite = currentWeaponSpriite;
     }

    public float GetCostToUpgrade(WeaponScriptableObjects weaponSO)
    {
        if (weaponSO.currentLevel < weaponSO.weaponsData.Length - 1)
        {

            costToUpgrade = weaponSO.weaponsData[weaponSO.currentLevel + 1].cost;
        }
        else
        {
            costToUpgrade = int.MaxValue;
        }
        return costToUpgrade;
    }

    public void BuyWeapon(int index)
    {
        print(this.name + " (" + transform.parent.name + ") : weapon with index " + index + " bought");
        weaponScriptableObjects[index].isBought = true;
        GameStats.currentGold -= weaponScriptableObjects[index].weaponsData[0].cost;//removing the cost
        currentWeapon = weaponScriptableObjects[index];
        GetWeaponData(currentWeapon.weaponsData);
    }
}