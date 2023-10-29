using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongRangeWeapon : MonoBehaviour
{
    public GameObject grenatePrefab;
    Granate grenate;
    public Text priceText;
    public float fireRate;
    public bool isBought;
    public int price;
    private float timer;
    private void Start()
    {
        grenate = grenatePrefab.GetComponent<Granate>();
        isBought = false;
        timer = 1f / fireRate;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && isBought && timer<0)
        {
            GameObject temp = Instantiate(grenatePrefab,transform.position,grenatePrefab.transform.rotation);
            temp.transform.parent = GameObject.Find("GrenateHolder").transform;
            timer = 1f / fireRate;
        }
        timer -= Time.deltaTime;
    }

    //public void BuyWeapon()
    //{
    //    gameObject.SetActive(true);
    //    GameObject meleeWeapon = GameObject.Find("Melee Weapon");
    //    meleeWeapon.SetActive(false);

    //    if(isBought) { return; }

    //    if(GameStats.currentGold >= price)
    //    {
    //        GameStats.currentGold -= price;
    //        isBought = true;
    //        priceText.text = "Bought";
    //    }
    //}
}
