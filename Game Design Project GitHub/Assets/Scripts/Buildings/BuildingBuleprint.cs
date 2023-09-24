using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingBuleprint : MonoBehaviour
{
    public int cost;
    public int sellPrice;
    public GameObject upgradedPrefab;
    public int upgradeCost;
    public Sprite spriteTOBuild;

    private void OnMouseDown()
    {
        if(ClickHandler.xDown)
        {
            Stats.currentGold += sellPrice;
            Destroy(this.gameObject);


            GameObject nearestTile = null;
            float nearestDistence = float.MaxValue;
            foreach (GameObject tile in GridSystem.tileArray)
            {
                float dis = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (dis < nearestDistence)
                {
                    nearestDistence = dis;
                    nearestTile = tile;
                }
            }
            if (nearestTile != null && nearestTile.GetComponent<Tile>().isOccupied == true)
            {
                nearestTile.GetComponent<Tile>().isOccupied = false;
            }
        }

        if(ClickHandler.vDown && Stats.currentGold >= upgradeCost && upgradedPrefab != null)
        {
            Stats.currentGold -= upgradeCost;
            Instantiate(upgradedPrefab, this.transform.position , upgradedPrefab.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
