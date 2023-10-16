using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapBlueprint : BuildingBuleprint
{
    

    [Header("Trap Details")]
    public EType trapType;
    public float enemySpeed = 0.5f;
    public float attackRate = 1.0f;
}
    public enum EType
    {
        None,
        SlowTrap,
        MagmaTrap
    }

