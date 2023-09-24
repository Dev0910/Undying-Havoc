using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{

    public int maxhealth = 100;
    int currenthealth;
    //public Text healthtext;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
    }

    public void Takedamage(int damage)
    {
        if (currenthealth > 0)
        {
            currenthealth -= damage;
            Debug.Log(currenthealth);
            //healthtext.text = currenthealth.ToString();
        }

     
       

        if (currenthealth <= 0)
        {
            currenthealth = 0;
            //healthtext.text = currenthealth.ToString();
            die();
        }
    }

    void die()
    {
        gameObject.SetActive(false);
        Debug.Log("Died");

    }


}
