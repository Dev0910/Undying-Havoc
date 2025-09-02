using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryManager : MonoBehaviour
{
    public Image displayImage;
    public TextMeshProUGUI displayText;

    public SingleResourse currentResource;
    // Start is called before the first frame update
    void Start()
    {
        currentResource = new SingleResourse();
        currentResource.amount = 1;
        currentResource.resource = EResources.Wood;
    }

    public void UpdateDisplayText()
    {
        displayText.text = currentResource.amount.ToString();
    }
    public void UpdateDisplayImage()
    {
        displayImage.sprite = GameManager.Instance.resourceManager.GetResourceSprite(currentResource.resource);
    }
    //remove all the resource in the building 
    public void ClearResource()
    {
        GameManager.Instance.gameStats.AddResourse(currentResource.resource,currentResource.amount);
        currentResource.amount = 0;
        currentResource.resource = EResources.None;
        UpdateDisplayImage();
        UpdateDisplayText();
    }
    //called by the buttons to add wood to the inventory
    public void AddWood(int _amount)
    {
        if(currentResource.resource == EResources.Bone) { return; }

        if(GameManager.Instance.gameStats.CheakIfResourseAvailable(EResources.Wood, _amount) && currentResource.amount + _amount <= 100)
        {
            GameManager.Instance.gameStats.RemoveResourse(EResources.Wood, _amount);
            currentResource.amount += _amount;
            currentResource.resource = EResources.Wood;
            UpdateDisplayImage();
            UpdateDisplayText();
        }
    }
    //called by the buttons to add stone to the inevtory
    public void AddBone(int _amount)
    {
        if (currentResource.resource == EResources.Wood) { return; }

        if (GameManager.Instance.gameStats.CheakIfResourseAvailable(EResources.Bone, _amount) && currentResource.amount + _amount <= 100)
        {
            GameManager.Instance.gameStats.RemoveResourse(EResources.Bone, _amount);
            currentResource.amount += _amount;
            currentResource.resource = EResources.Bone;
            UpdateDisplayImage();
            UpdateDisplayText();
        }
    }

}
