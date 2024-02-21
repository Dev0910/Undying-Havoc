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
    public string name;
    public string description;
    public Sprite buildingSprite;
    public Sprite bulletSprite;
    public int health;
    public int damage;
    public float range;

    [Header("Resourse Required To Upgrade")]
    public SingleResourse resourse;

    [Header("Resourse Recived On Selling The Building")]
    public SingleResourse sellResourse;
}
