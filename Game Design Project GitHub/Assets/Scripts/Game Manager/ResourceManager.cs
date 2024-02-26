using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// just a script to initialize all the enums and class 
public class ResourceManager : MonoBehaviour
{
    public List<ResourcesScriptableObject> allresource = new List<ResourcesScriptableObject>();
    public Sprite GetResourceSprite(EResources eResource)
    {
        Sprite _sprite = null;

        foreach (ResourcesScriptableObject resourceSo in allresource)
        {
            if(resourceSo != null)
            {
                if(resourceSo.resourceName == eResource)
                {
                    _sprite = resourceSo.sprite;
                    break;
                }
            }
        }
            return _sprite;
    }
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
