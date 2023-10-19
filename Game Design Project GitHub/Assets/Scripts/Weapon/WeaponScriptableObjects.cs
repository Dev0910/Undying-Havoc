using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponScriptableObjects : ScriptableObject
{
    public bool isBought;
    public int currentLevel;
    public Vector2 colliderOffSet;
    public Vector2 colliderScale;
    public WeaponData[] weaponsData;
}

[System.Serializable]
public class WeaponData
{
    //public string name;
    public Sprite weaponSprite;
    public int cost;
    public float damage;
}

