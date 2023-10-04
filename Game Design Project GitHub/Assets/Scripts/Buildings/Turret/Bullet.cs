using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    public int damage = 50;
    //public GameObject impactEffect;
    public float speed;
    public void seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            gameObject.SetActive(false);
            Turret.qbullet.Enqueue(gameObject);
            return;
        }

        Vector2 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Enemy1 e = target.GetComponent<Enemy1>();
        if (e != null)
        {
            e.TakeDamage(damage);
            Debug.Log("Damage To Enemy");
        }

        //Destroy(effect, 2f);
        //Destroy(gameObject);

        gameObject.SetActive(false);
        Turret.qbullet.Enqueue(gameObject);
    }
}
