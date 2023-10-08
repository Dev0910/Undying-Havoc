using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponScriptableObjects : ScriptableObject
{
    public WeaponData[] weaponsData;
}

[System.Serializable]
public class WeaponData
{
    public Sprite weaponSprite;
    public int cost;
    public int damage;
}

