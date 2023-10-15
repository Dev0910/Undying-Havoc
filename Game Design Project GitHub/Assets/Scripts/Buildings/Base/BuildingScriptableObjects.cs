using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class BuildingScriptableObjects : ScriptableObject
{
    public BuildingData[] buildingData;
}

[System.Serializable]
public class BuildingData
{
    public Sprite buildingSprite;
    public Sprite bulletSprite;
    public int health;
    public int cost;
    public int sellingPrice;
    public int damage;
}
