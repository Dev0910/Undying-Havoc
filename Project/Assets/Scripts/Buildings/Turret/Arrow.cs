using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Arrow/Bulled shooted by the turret
public class Arrow : MonoBehaviour
{
    public Transform target;

    public int damage = 50;
    public float speed;
    //update the taeget
    public void seek(Transform _target, Sprite sprite , int _damage)
    {
        target = _target;
        damage = _damage;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void Update()
    {
        if (target == null)//if there is no target
        {
            gameObject.SetActive(false);
            //ArrowTurret.qbullet.Enqueue(gameObject);//add to the bullet queue
            PoolManager.Instance.AddToPool(this.gameObject, EPool.Bullet);
            return;
        }

        Vector2 direction = target.position - transform.position; //direction from bullet to target
        float distanceThisFrame = speed * Time.deltaTime;//calculate the distance to travel this frame

        if (direction.magnitude <= distanceThisFrame)//if distance from target is less then distance travel this frame
        {
            HitTarget();//call the hit function
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);//move towards the target
    }

    void HitTarget()
    {
        //GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Enemy e = target.GetComponent<Enemy>();//creating a instence of the Enemy1 script
        if (e != null)
        {
            e.TakeDamage(damage);//calling the take damage function from the enemy script
            //Debug.Log("Damage To Enemy");
        }

        //Destroy(effect, 2f);
        //Destroy(gameObject);

        gameObject.SetActive(false);
        //ArrowTurret.qbullet.Enqueue(gameObject);//adding to the bullet queue
        PoolManager.Instance.AddToPool(this.gameObject,EPool.Bullet);
    }
}
