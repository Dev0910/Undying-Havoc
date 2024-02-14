using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BuildingBuleprint
{
    // Start is called before the first frame update
    void Start()
    {
        childGameobejct = this.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildingData = buildingScriptableObjects.buildingData;
        currentLevel = 0;
        GetData();
        spriteRenderer.sprite = currentSpriite;
        nearestTile = GameManager.Instance.gridSystem.GetNearestTile(this.transform.position);//geting the nearest tile by calling the function in class GridSystem

    }
}
