using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellOrUpgradeUI : MonoBehaviour
{
    public GameObject temp;
    // Start is called before the first frame update
    public void Start()
    {
        temp = this.gameObject;
    }
    public void Sell()
    {
        //GameManager.currentGold += gameObject.GetComponent<BuildingBuleprint>().sellPrice;
        Destroy(temp);
        //gameObject.SetActive(false);
    }
}
