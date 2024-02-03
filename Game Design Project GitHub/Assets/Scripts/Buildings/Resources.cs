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
    Iron
}
public enum Esource
{
    None,
    Tree,
    Rock,
    IronOre
}
