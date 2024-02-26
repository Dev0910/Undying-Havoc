using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public static bool xDown;
    public static bool vDown;
    public static bool eDown;
    public GameObject meleeWeapon;
    

    private void Start()
    {
        meleeWeapon = GameObject.Find("Melee Weapon");
        
        meleeWeapon.SetActive(false);
        //longRangeWeapon.SetActive(false);
    }
    void Update()
    {
        //cheak for X input
        if (Input.GetKey(KeyCode.X)) { xDown = true; } else if (!Input.GetKey(KeyCode.X)) { xDown = false; }
        //cheak for V input
        if (Input.GetKey(KeyCode.V)) { vDown = true; } else if (!Input.GetKey(KeyCode.V)) { vDown = false; }
        //cheak foe E input
        if (Input.GetKey(KeyCode.E)) { eDown = true; } else if (!Input.GetKey(KeyCode.E)) { eDown = false; }

        //switch weapon according to the number pressed

        //cheak for number 1 input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            meleeWeapon.SetActive(true);
            Weapon weapon = meleeWeapon.GetComponent<Weapon>();
            weapon.SwitchWeapon(1);
        }
        //cheak for number 2 input
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            meleeWeapon.SetActive(true);
            Weapon weapon = meleeWeapon.GetComponent<Weapon>();
            weapon.SwitchWeapon(2);
        }
        //cheak for number 3 input
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            meleeWeapon.SetActive(true);
            Weapon weapon = meleeWeapon.GetComponent<Weapon>();
            weapon.SwitchWeapon(3);
        }
        //cheak for number 4 input
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            meleeWeapon.SetActive(true);
            Weapon weapon = meleeWeapon.GetComponent<Weapon>();
            weapon.SwitchWeapon(4);
        }
    }

}
