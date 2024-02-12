using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponScriptableObjects : ScriptableObject
{
    public bool isBought;
    public int currentLevel;
    //public string currentName;
    public Vector2 colliderOffSet;
    public Vector2 colliderScale;
    public Vector3 weaponPosition;
    public WeaponData[] weaponsData;
}

[System.Serializable]
public class WeaponData
{
    //public string name;
    public string name;
    public Sprite weaponSprite;
    public EResources resource;
    public int cost;
    public float damage;
    public int damageOnWood;
    public int damageOnStone;
    public int damageOnIron;
}

