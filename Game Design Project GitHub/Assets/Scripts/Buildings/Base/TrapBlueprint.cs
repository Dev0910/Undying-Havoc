using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlueprint : BuildingBuleprint
{
    [Header("Trap Details")]
    [SerializeField] private EType trapType;
    [SerializeField] private float enemySpeed = 0.5f;
    [SerializeField] private float attackRate = 1.0f;

    private List<Collider2D> targets;
    private float attackTimer;
    void Start()
    {
        nearestTile = GameManager.Instance.gridSystem.GetNearestTile(this.transform.position);//geting the nearest tile by calling the function in class GridSystem
        targets = new List<Collider2D>();
        attackTimer = 0;
    }

    private void Update()
    {
        if (trapType == EType.MagmaTrap && targets.Count > 0)
        {
            if(attackTimer <= 0)
            {
                attackTimer = 1f / attackRate;
                foreach(Collider2D target in targets)
                {
                    if (target != null)
                    {
                        target.gameObject.GetComponent<BaseEnemy>().TakeDamage(damage);
                    }
                }
            }
            attackTimer -= Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            switch (trapType)
            {
                case EType.None:
                    {
                        break;
                    }
                case EType.SlowTrap:
                    {
                        
                        collision.gameObject.GetComponent<Enemy>().ChangeMoveSpeed(enemySpeed);
                        break;
                    }
                case EType.MagmaTrap:
                    {
                        collision.gameObject.GetComponent<Enemy>().ChangeMoveSpeed(enemySpeed);
                        targets.Add(collision);
                        break;
                    }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            switch (trapType)
            {
                case EType.None:
                    {
                        break;
                    }
                case EType.SlowTrap:
                    {
                        
                        collision.gameObject.GetComponent<Enemy>().DefaultMoveSpeed();
                        break;
                    }
                case EType.MagmaTrap:
                    {
                        collision.gameObject.GetComponent<Enemy>().DefaultMoveSpeed();
                        if (targets.Count > 0 && collision)
                        {
                            targets.Remove(collision);   
                        }
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

