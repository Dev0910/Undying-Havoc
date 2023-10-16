using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapBlueprint : BuildingBuleprint
{
    [Header("Trap Details")]
    [SerializeField] public EType trapType;
    [SerializeField] public float enemySpeed = 0.5f;
    [SerializeField] public float attackRate = 1.0f;

    //public List<GameObject> targets;
    //private float attackTimer;
//    void Start()
//    {
//        nearestTile = GameManager.Instance.gridSystem.GetNearestTile(this.transform.position);//geting the nearest tile by calling the function in class GridSystem
//        //targets = new List<GameObject>();
//        //attackTimer = 0;
//    }

//    //private void Update()
//    //{
//    //    if (trapType == EType.MagmaTrap && targets.Count > 0)
//    //    {
//    //        if (attackTimer <= 0)
//    //        {
//    //            attackTimer = 1f / attackRate;
//    //            foreach (GameObject target in targets)
//    //            {
//    //                if (target != null)
//    //                {
//    //                    target.GetComponent<BaseEnemy>().TakeDamage(damage, this.gameObject);
//    //                }
//    //            }
//    //        }
//    //        attackTimer -= Time.deltaTime;
//    //    }
//    //}


//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Enemy"))
//        {
//            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
//            switch (trapType)
//            {
//                case EType.None:
//                    {
//                        break;
//                    }
//                case EType.SlowTrap:
//                    {

//                        enemy.ChangeMoveSpeed(enemySpeed);
//                        break;
//                    }
//                case EType.MagmaTrap:
//                    {
//                        enemy.ChangeMoveSpeed(enemySpeed);
//                        enemy.UpdateMagmaTrap(true, this.gameObject);
//                        //targets.Add(collision.gameObject);
//                        break;
//                    }
//            }
//        }
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Enemy"))
//        {
//            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
//            switch (trapType)
//            {
//                case EType.None:
//                    {
//                        break;
//                    }
//                case EType.SlowTrap:
//                    {

//                        enemy.DefaultMoveSpeed();
//                        break;
//                    }
//                case EType.MagmaTrap:
//                    {
//                        enemy.DefaultMoveSpeed();
//                        enemy.UpdateMagmaTrap(false, this.gameObject);
//                        //if (targets.Count > 0 && collision)
//                        //{
//                        //    targets.Remove(collision.gameObject);
//                        //}
//                        break;
//                    }
//            }
//        }
//    }
}

    


    public enum EType
    {
        None,
        SlowTrap,
        MagmaTrap
    }

