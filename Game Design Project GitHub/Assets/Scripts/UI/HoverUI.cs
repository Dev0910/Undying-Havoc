using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HoverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI details;
    [SerializeField] private TextMeshProUGUI sellprice;
    [SerializeField] private TextMeshProUGUI upgradecost;
    [SerializeField] private Image healthbar;
    [SerializeField] private Image sellResourseImage;
    [SerializeField] private Image upgradeResourseImage;

    public void UpdateName(string name)
    {
        buildingName.text = name;
    }
    public void UpdateDetails(string _details)
    {
        details.text = _details;
    }
    public void UpdateHealthBar(float fillAmount)
    {
        healthbar.fillAmount = fillAmount;
    }
    public void UpdateSellingPrice(EResources resource,int price)
    {
        sellprice.text = ": " + price.ToString();
        sellResourseImage.sprite = GameManager.Instance.resourceManager.GetResourceSprite(resource);
    }

    public void UpdateUpgradeCost(EResources resource, int cost,bool isMax)
    {
        if(isMax)
        {
            upgradecost.text = "Max Level";
            upgradeResourseImage.gameObject.SetActive(false);
        }
        else
        {
            upgradeResourseImage.gameObject.SetActive(true);
            upgradecost.text = ": " + cost.ToString();
            upgradeResourseImage.sprite = GameManager.Instance.resourceManager.GetResourceSprite(resource);
        }

    }
}
