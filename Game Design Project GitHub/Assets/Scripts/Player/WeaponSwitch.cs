using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedweapon = 0;
    float MouseInput;
    


    // Start is called before the first frame update
    void Start()
    {
        selectweapon();
    }

    // Update is called once per frame
    void Update()
    {
        MouseInput = Input.GetAxis("Mouse ScrollWheel");
       
        if (MouseInput!=0)
        {
            Switch();
        }
        else
        {
            return;
        }
        
    }

    void selectweapon()
    {
        int i = 0;
        foreach(Transform weapon in transform) 
        {
            if (i == selectedweapon) 
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
    void Switch()
    {
        int previousselectedweapon = selectedweapon;

        if (MouseInput > 0f)
        {
            if (selectedweapon >= transform.childCount - 1)
            {
                selectedweapon = 0;
            }
            else
            {
                selectedweapon++;
            }

        }

        if (MouseInput < 0f)
        {
            if (selectedweapon <= 0)
            {
                selectedweapon = transform.childCount - 1;
            }
            else
            {
                selectedweapon--;
            }

        }
        if (previousselectedweapon != selectedweapon)
        {
            selectweapon();
        }
    }

}

