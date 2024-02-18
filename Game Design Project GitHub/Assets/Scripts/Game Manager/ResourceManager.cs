using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public List<ResourcesScriptableObject> allresource = new List<ResourcesScriptableObject>();
}
public class ResourceSourses : MonoBehaviour
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
    public int amount;
}
