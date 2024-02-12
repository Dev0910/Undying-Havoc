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
    private GameObject resourcesInRange;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        enemyToAttack = null;
        resourcesInRange = null;
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && resourcesInRange != null)
        {
            Resources resources = resourcesInRange.GetComponent<Resources>();
            CollectResources(resources);
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
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyToAttack = collision.gameObject;
        }
        if(collision.gameObject.tag == "Sourse")
        {
            resourcesInRange = collision.gameObject;
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
            resourcesInRange = null;
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
            shopButton.UpdatePrices(weaponsData[currentWeapon.currentLevel].resource, costToUpgrade, false);
            
        }
        else
        {
            shopButton.UpdatePrices(weaponsData[currentWeapon.currentLevel].resource, costToUpgrade, true);
        }

        shopButton.UpdateName(currentWeaponName, currentWeaponSpriite);
        shopButton.UpdateDamage(damage,damageOnWood,damageOnStone,damageOnIron);
        
        transform.localPosition = currentWeapon.weaponPosition;
        boxCollider2D.enabled = true;
        boxCollider2D.offset = currentWeapon.colliderOffSet;
        boxCollider2D.size = currentWeapon.colliderScale;
        spriteRenderer.sprite = currentWeaponSpriite;
        
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

    private void CollectResources(Resources resources)
    {
        EResources rName = resources.eResource;
        Esource sourseName = resources.eSource;
        if (resources.health <= 0) { return;}
        GameStats gs = GameManager.Instance.gameStats;
        switch (sourseName)
        {
            case Esource.None: break;
                
            case Esource.Tree:
                {
                    resources.health -= damageOnWood;
                    gs.wood += damageOnWood;
                    gs.woodText.text = ": " + gs.wood;
                }break;
            case Esource.Rock:
                {
                    resources.health -= damageOnStone;
                    gs.stone += damageOnStone;
                    gs.stoneText.text = ": " + gs.stone;
                }
                break;
            case Esource.IronOre:
                {
                    resources.health -= damageOnIron;
                    gs.iron += damageOnIron;
                    gs.ironText.text = ": " + gs.iron;
                }
                break;
        }
        if(resources.health <= 0) { Destroy(resources.gameObject); }

    }
}