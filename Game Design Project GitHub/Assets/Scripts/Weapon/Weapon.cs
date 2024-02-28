using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Weapon : MonoBehaviour
{
    public ShopButtons[] shopButtons;
    public WeaponScriptableObjects[] weaponScriptableObjects;
    //public float attackrange; // Range for the weapon
    //public LayerMask enemylayers; // Enemy Layer for attacking the enemy
    public string currentWeaponName;
    public Sprite currentWeaponSpriite; // Base weapon Sprite
    public EResources resourseRequired;
    //public int costToBuy; // cost to buy the weapon
    public float damage; // Damage dealt by weapon
    public Sprite upgradedSprite; // New sprite for Upgraded Weapon
    public int costToUpgrade; // cost to upgrade the weapon
    public int damageOnWood; //damage that will be done on the tree
    public int damageOnStone; //damage that will be done on the Rock
    public int damageOnIron;//damage that will be done on the iron ore
    
    private WeaponScriptableObjects currentWeapon;// Taking reference for the scriptable Objects
    private GameObject enemyToAttack;
    private GameObject sourceInRange;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        enemyToAttack = null;
        sourceInRange = null;
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
            BaseEnemy baseEnemy = enemyToAttack.GetComponent<BaseEnemy>();
            baseEnemy.TakeDamage(damage);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && sourceInRange != null)
        {
            Source source = sourceInRange.GetComponent<Source>();
            DropResources(source);
        }

    }

    public void SwitchWeapon(int numberPressed)
    {
        if(currentWeapon == null) { return; }


        if(numberPressed == 1 && weaponScriptableObjects[numberPressed-1].isBought)
        {
            currentWeapon = weaponScriptableObjects[0];
            GetWeaponData(currentWeapon.weaponsData, shopButtons[0]);
        }
        else if(numberPressed == 2 && weaponScriptableObjects[numberPressed - 1].isBought)
        {
            currentWeapon = weaponScriptableObjects[1];
            GetWeaponData(currentWeapon.weaponsData, shopButtons[1]);
        }
        else if (numberPressed == 3 && weaponScriptableObjects[numberPressed - 1].isBought)
        {
            currentWeapon = weaponScriptableObjects[2];
            GetWeaponData(currentWeapon.weaponsData, shopButtons[2]);
        }
        else if (numberPressed == 4 && weaponScriptableObjects[numberPressed - 1].isBought)
        {
            currentWeapon = weaponScriptableObjects[3];
            GetWeaponData(currentWeapon.weaponsData, shopButtons[3]);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyToAttack = collision.gameObject;
        }
        if(collision.gameObject.tag == "Sourse")
        {
            sourceInRange = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyToAttack = null;
        }
        if (collision.gameObject.tag == "Sourse")
        {
            sourceInRange = null;
        }
    }

    void GetWeaponData(WeaponData[] weaponsData,ShopButtons shopButton)
    {
        currentWeaponName = weaponsData[currentWeapon.currentLevel].name;
        currentWeaponSpriite = weaponsData[currentWeapon.currentLevel].weaponSprite;
        resourseRequired = weaponsData[currentWeapon.currentLevel].resource;
        
        damage = weaponsData[currentWeapon.currentLevel].damage;
        damageOnWood = weaponsData[currentWeapon.currentLevel].damageOnWood;
        damageOnStone = weaponsData[currentWeapon.currentLevel].damageOnStone;
        damageOnIron = weaponsData[currentWeapon.currentLevel].damageOnIron;
        if (currentWeapon.currentLevel < weaponsData.Length - 1)
        {
            upgradedSprite = weaponsData[currentWeapon.currentLevel].weaponSprite;
            costToUpgrade = weaponsData[currentWeapon.currentLevel + 1].cost;
        }
        UpdateShopPrices(weaponsData, shopButton);

        transform.localPosition = currentWeapon.weaponPosition;
        boxCollider2D.enabled = true;
        boxCollider2D.offset = currentWeapon.colliderOffSet;
        boxCollider2D.size = currentWeapon.colliderScale;
        spriteRenderer.sprite = currentWeaponSpriite;
        
    }
    private void UpdateShopPrices(WeaponData[] weaponsData, ShopButtons shopButton)
    {
        
        if (currentWeapon.currentLevel < weaponsData.Length - 1)
        {
            WeaponData wd = weaponsData[currentWeapon.currentLevel + 1];
            shopButton.UpdatePrices(wd.resource, costToUpgrade, false);
            shopButton.UpdateName(wd.name, wd.weaponSprite);
            shopButton.UpdateDamage(wd.damage, wd.damageOnWood, wd.damageOnStone, wd.damageOnIron);

        }
        else
        {
            shopButton.UpdatePrices(weaponsData[currentWeapon.currentLevel].resource, costToUpgrade, true);
        }
    }
    public bool CanBuyWeapon(WeaponScriptableObjects weaponSO)
    {
        bool canBuy = false;
        if (!weaponSO.isBought)
        {
            canBuy = GameManager.Instance.gameStats.CheakIfResourseAvailable(weaponSO.weaponsData[0].resource, weaponSO.weaponsData[0].cost);
        }
        else
        {
            if (weaponSO.currentLevel < weaponSO.weaponsData.Length)
            {
                canBuy = GameManager.Instance.gameStats.CheakIfResourseAvailable(weaponSO.weaponsData[weaponSO.currentLevel+1].resource, weaponSO.weaponsData[weaponSO.currentLevel+1].cost);
            }
        }
        return canBuy;
    }
    public void BuyWeapon(int index)
    {
        weaponScriptableObjects[index].isBought = true;
        GameManager.Instance.gameStats.RemoveResourse(weaponScriptableObjects[index].weaponsData[0].resource, weaponScriptableObjects[index].weaponsData[0].cost);
        currentWeapon = weaponScriptableObjects[index];
        GetWeaponData(currentWeapon.weaponsData, shopButtons[index]);

    }

    public void UpgradeWeapon(int index)
    {
        if (weaponScriptableObjects[index].currentLevel < weaponScriptableObjects[index].weaponsData.Length)
        {
            weaponScriptableObjects[index].currentLevel++;
            GameManager.Instance.gameStats.RemoveResourse(weaponScriptableObjects[index].weaponsData[weaponScriptableObjects[index].currentLevel].resource, weaponScriptableObjects[index].weaponsData[weaponScriptableObjects[index].currentLevel].cost);
            currentWeapon = weaponScriptableObjects[index];
            GetWeaponData(currentWeapon.weaponsData, shopButtons[(int)index]);
        }
    }

    private void DropResources(Source source)
    {
        switch (source.source)
        {
            case Esource.None: break;

            case Esource.Tree:
                {
                    source.DropResources(damageOnWood);
                }
                break;
            case Esource.Rock:
                {
                    source.DropResources(damageOnStone);
                }
                break;
            case Esource.IronOre:
                {
                    source.DropResources(damageOnIron);
                }
                break;
        }
    }
}