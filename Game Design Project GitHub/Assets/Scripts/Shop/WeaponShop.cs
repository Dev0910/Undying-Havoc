using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    //buys weapon for the player
    //this function is called by the buttons in UI
    public void BuyWeapon(GameObject prefab)
    {
        //cheaks the conditions to buy the weapon
        if (prefab.GetComponent<Weapon>().costToBuy <= GameStats.currentGold /*&& prefab.GetComponent<Weapon>().isBought == false*/)
        {
            GameStats.currentGold -= prefab.GetComponent<Weapon>().costToBuy;//reduse the weapon cost
            GameObject instantiatedPrefab = Instantiate(prefab, GameManager.Instance.player.transform.position, GameManager.Instance.player.transform.rotation);//spawns the weapon
            instantiatedPrefab.transform.parent = GameObject.FindWithTag("WeaponHolder").transform;//set player as the parent gameobject
            prefab.GetComponent<Weapon>().isBought = true;
        }


        // Checks the conditions to upgrade the weapon
        if (prefab.GetComponent<Weapon>().costToUpgrade <= GameStats.currentGold && prefab.GetComponent<Weapon>().isBought == true)
        {
            prefab.GetComponent<Weapon>().UpgradeWeapon();
        }

    }
}


