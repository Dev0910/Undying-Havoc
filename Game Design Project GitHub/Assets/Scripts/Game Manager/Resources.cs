using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public EResources eResource;
    public Esource eSource;
    public int health;
}
public enum EResources
{
    None,
    Wood,
    Stone,
    Iron,
    Bone
}
public enum Esource
{
    None,
    Tree,
    Rock,
    IronOre,
    Monsters
}

[System.Serializable]
public class SingleResourse
{
    public EResources resource;
    public GameObject prefab;
    public int amount;
}


