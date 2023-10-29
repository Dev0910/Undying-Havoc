using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    public ShopButtons[] shopButtons;
    public GameObject meleeWeapon;
    public GameObject longRangeWeapon;
    public void BuyWeapon(int index)
    {
        meleeWeapon.SetActive(true);
        longRangeWeapon.SetActive(false);
        Weapon weapon = meleeWeapon.GetComponent<Weapon>();
        WeaponScriptableObjects weaponSO = weapon.weaponScriptableObjects[index];
        bool canBuy = weaponSO.weaponsData[weaponSO.currentLevel].cost <= GameStats.currentGold ? true : false;
        if (canBuy && !weaponSO.isBought)
        {
            //print("weapon shold buy");
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
    public void BuyGrenate()
    {
        meleeWeapon.SetActive(false );
        longRangeWeapon.SetActive(true);
        LongRangeWeapon lrw = longRangeWeapon.GetComponent<LongRangeWeapon>();
        if (lrw.currentlevel >= lrw.grenateDatas.Length-1) 
        {

            shopButtons[3].UpdateButton("Grenate", lrw.currentlevel + 1, lrw.grenateDatas[lrw.currentlevel].damage, lrw.grenateDatas[lrw.currentlevel].price, true);
            return; 
        }


        if(GameStats.currentGold >= lrw.grenateDatas[lrw.currentlevel+1].price && !lrw.isBought)
        {
            GameStats.currentGold -= lrw.grenateDatas[lrw.currentlevel + 1].price;
            lrw.isBought = true;
            shopButtons[3].UpdateButton("Grenate", lrw.currentlevel + 2, lrw.grenateDatas[lrw.currentlevel+1].damage, lrw.grenateDatas[lrw.currentlevel + 1].price, false);
            lrw.currentlevel++;
        }
        else if(GameStats.currentGold >= lrw.grenateDatas[lrw.currentlevel + 1].price && lrw.isBought)
        {
            GameStats.currentGold -= lrw.grenateDatas[lrw.currentlevel + 1].price;
            shopButtons[3].UpdateButton("Grenate", lrw.currentlevel + 2, lrw.grenateDatas[lrw.currentlevel + 1].damage, lrw.grenateDatas[lrw.currentlevel + 1].price, false);
            lrw.currentlevel++;
        }
        
    }
}

