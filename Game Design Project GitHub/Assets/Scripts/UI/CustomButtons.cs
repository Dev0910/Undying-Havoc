using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomButtons : MonoBehaviour
{
    private Image image;
    public SingleResourse resourse;
    public Color startColor;
    public Color onHoverColor;
    public Color onClickColor;
    InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("Oxygen Generator").GetComponent<InventoryManager>();
    }
    private void OnMouseDown()
    {
        AddResource();
    }

    private void AddResource()
    {
        switch (resourse.resource)
        {
            case EResources.Wood:
                {
                    inventoryManager.AddWood(resourse.amount);
                    break;
                }
            case EResources.Bone:
                {
                    inventoryManager.AddBone(resourse.amount);
                    break;
                }
        }
    }
}




