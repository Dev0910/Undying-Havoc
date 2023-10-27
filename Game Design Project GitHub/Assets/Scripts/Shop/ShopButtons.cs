using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopButtons : MonoBehaviour
{
    public Text currentName;
    public Text level;
    public Text damage;
    public Text price;
    public void UpdateButton(string _name,int _level,float _damge,int _price,bool isMax)
    {
        if(!isMax)
        {
            currentName.text = _name;
            level.text = "Level : " + _level;
            damage.text = "Damage : " + _damge;
            price.text = "Price : " + _price;
        }
        else
        {
            price.text = "Max Level";
        }
        
    }
}
