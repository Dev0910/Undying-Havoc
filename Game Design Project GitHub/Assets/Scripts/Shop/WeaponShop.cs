using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    private int weaponCount;
    private void Start()
    {
        weaponCount = 0;
    }
    //buys weapon for the player
    //this function is called by the buttons in UI
    public void BuyWeapon(GameObject prefab)
    {
        //cheaks the conditions to buy the weapon
        if (prefab.GetComponent<Weapon>().costToBuy <= GameStats.currentGold)
        {
            GameObject temp = GameObject.Find(prefab.name);
            if(temp == null)
            {
                SpawnWeapon(prefab);
            }
            else
            {
                temp.GetComponent<Weapon>().UpgradeWeapon();
            }
        }
    }

    private void SpawnWeapon(GameObject prefab)
    {
        GameStats.currentGold -= prefab.GetComponent<Weapon>().costToBuy;//reduse the weapon cost
        GameObject instantiatedPrefab = Instantiate(prefab, GameManager.Instance.weaponHolder.transform.position, GameManager.Instance.weaponHolder.transform.rotation);//spawns the weapon
        instantiatedPrefab.name = prefab.name;
        instantiatedPrefab.transform.parent = GameObject.Find("WeaponHolder").transform;//set player as the parent

        
        instantiatedPrefab.SetActive(false);
        if (prefab.name == "Axe")
        {
            instantiatedPrefab.transform.rotation = Quaternion.Euler(0, 0, -240); // Set rotation to identity
        }

        if (prefab.name == "Sword")
        {
            instantiatedPrefab.transform.rotation = Quaternion.Euler(0, 0, -223); // Set rotation to identity
        }
    }
}


