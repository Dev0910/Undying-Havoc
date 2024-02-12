using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopButtons : MonoBehaviour
{
    public TextMeshProUGUI currentName;
    public Image currentImage;
    public TextMeshProUGUI damageToMonsters;
    public TextMeshProUGUI damageToTree;
    public TextMeshProUGUI damageToRock;
    public TextMeshProUGUI damageToIronore;
    public Image currentResourseImage;
    public TextMeshProUGUI priceAmount;
    public Button buyButton;

    public void UpdateName(string _name,Sprite _currentImage)
    {
        currentName.text = _name;
        currentImage.sprite = _currentImage;
    }
    public void UpdateDamage(float _damage,int tree,int rock,int ironOre)
    {
        damageToMonsters.text = "Monster : " + _damage;
        damageToTree.text = "Tree : " + tree;
        damageToRock.text = "Rock : " + rock;
        damageToIronore.text = "Iron Ore : " + ironOre;
    }
    public void UpdatePrices(EResources resourse,int price,bool isMax)
    {
        
        if(isMax)
        {
            priceAmount.text = "Max Level";
            buyButton.interactable = false;
            currentResourseImage.sprite = null;
            return;
        }
        buyButton.interactable = true;
        //currentResourseImage.sprite = resourse;
        priceAmount.text = ": " + price;
    }
}
