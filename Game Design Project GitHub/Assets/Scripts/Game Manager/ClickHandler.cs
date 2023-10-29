using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public static bool xDown;
    public static bool vDown;
    public GameObject meleeWeapon;
    public GameObject longRangeWeapon;

    private void Start()
    {
        meleeWeapon = GameObject.Find("Melee Weapon");
        longRangeWeapon = GameObject.Find("Long Range Weapon");
        meleeWeapon.SetActive(false);
        longRangeWeapon.SetActive(false);
    }
    void Update()
    {
        //cheak for X input
        if (Input.GetKey(KeyCode.X)) { xDown = true; } else if (!Input.GetKey(KeyCode.X)) { xDown = false; }
        //cheak for V input
        if (Input.GetKey(KeyCode.V)) { vDown = true; } else if (!Input.GetKey(KeyCode.V)) { vDown = false; }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            meleeWeapon.SetActive(true);
            longRangeWeapon.SetActive(false);
            Weapon weapon = meleeWeapon.GetComponent<Weapon>();
            weapon.SwitchWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            meleeWeapon.SetActive(true);
            longRangeWeapon.SetActive(false);
            Weapon weapon = meleeWeapon.GetComponent<Weapon>();
            weapon.SwitchWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            meleeWeapon.SetActive(true);
            longRangeWeapon.SetActive(false);
            Weapon weapon = meleeWeapon.GetComponent<Weapon>();
            weapon.SwitchWeapon(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            meleeWeapon.SetActive(false);
            longRangeWeapon.SetActive(true);
        }
    }

}
