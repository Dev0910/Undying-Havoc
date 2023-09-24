using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;

    [Header("shooting")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCooldown = 1f;

    [Header("unity things")]
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;

    public static Queue<GameObject> qbullet;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        qbullet = new Queue<GameObject>();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
        fireCooldown -= Time.deltaTime;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Shoot()
    {
        GameObject bulletGO;

        if (CheakQueue())
        {
            bulletGO = qbullet.Dequeue();
            bulletGO.SetActive(true);
            bulletGO.transform.position = transform.position;
        }
        else
        {
            bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        }

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.seek(target);
        }
    }

    bool CheakQueue()
    {
        if (qbullet.Count > 0)
        {
            return true;
        }
        return false;

    }
}
