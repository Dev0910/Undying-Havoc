using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Resource")]
public class ResourcesScriptableObject : ScriptableObject
{
    public EResources resourceName;
    public Sprite sprite;
}
