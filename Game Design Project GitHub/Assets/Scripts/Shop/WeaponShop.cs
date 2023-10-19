using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    public void BuyWeapon(int index)
    {
        
        Weapon weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
        WeaponScriptableObjects weaponSO = weapon.weaponScriptableObjects[index];
        bool canBuy = weaponSO.weaponsData[weaponSO.currentLevel].cost <= GameStats.currentGold ? true : false;
        if (canBuy && !weaponSO.isBought)
        {
            print("weapon shold buy");
            weapon.BuyWeapon(index);
        }
        else if (canBuy && weaponSO.isBought && weapon.GetCostToUpgrade(weaponSO) <= GameStats.currentGold)
        {
            weapon.UpgradeWeapon(index);
        }
        else
        {
            print("Can not but");
        }

    }
}

