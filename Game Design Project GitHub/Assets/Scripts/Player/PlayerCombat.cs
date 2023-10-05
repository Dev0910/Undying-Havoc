using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int damagetaken = 20;
    public float attackrange = 0.4f;
    public LayerMask enemylayers;
    public int costToBuy;
    public int costToUpgrade;
   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {

        Collider2D [] hitenemies = Physics2D.OverlapCircleAll(transform.position , attackrange ,enemylayers);

        foreach(Collider2D enemy in hitenemies) 
        {
            enemy.GetComponent<Enemy1>().TakeDamage(damagetaken);
        }  
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackrange);   
    }
}
