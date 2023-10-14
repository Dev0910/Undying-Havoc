using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlueprint : BuildingBuleprint
{
    [Header("Trap Blueprint")]
    public EType type;

    [Header("Slow Trap")]
    public float enemySpeed = 0.5f;

    void Start()
    {
        nearestTile = GameManager.Instance.gridSystem.GetNearestTile(this.transform.position);//geting the nearest tile by calling the function in class GridSystem
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            switch (type)
            {
                case EType.None:
                    {
                        break;
                    }
                case EType.SlowTrap:
                    {
                        Debug.Log("Slow Enemy");
                        collision.gameObject.GetComponent<Enemy1>().ChangeMoveSpeed(enemySpeed);
                        break;
                    }
                case EType.MagmaTrap:
                    {
                        break;
                    }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            switch (type)
            {
                case EType.None:
                    {
                        break;
                    }
                case EType.SlowTrap:
                    {
                        Debug.Log("Enemy Defoult speed");
                        collision.gameObject.GetComponent<Enemy1>().DefaultMoveSpeed();
                        break;
                    }
                case EType.MagmaTrap:
                    {
                        break;
                    }
            }
        }
    }
}

    


    public enum EType
    {
        None,
        SlowTrap,
        MagmaTrap
    }

