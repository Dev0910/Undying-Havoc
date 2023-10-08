using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTurret : BuildingBuleprint
{
    public Transform target;

    [Header("shooting")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCooldown = 1f;

    [Header("unity things")]
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;

    public static Queue<GameObject> qbullet;//creating the bullet queue for object pooling

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildingData = buildingScriptableObjects.buildingData;
        currentLevel = 0;
        GetData();
        spriteRenderer.sprite = currentSpriite;
        nearestTile = GameManager.Instance.gridSystem.GetNearestTile(this.transform.position);//geting the nearest tile by calling the function in class GridSystem
        InvokeRepeating("UpdateTarget", 0f, 0.5f);//updating target every 0.5sec
        qbullet = new Queue<GameObject>();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);//creating array to store all the enemys
        float shortestDistance = Mathf.Infinity;// to storing the shortest distance
        GameObject nearestEnemy = null;// to store the nearest enemy
        foreach (GameObject enemy in enemies)//run for each enemy stored in the enemies array
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);//calculate and temporary store the distance of the enemies and the turret
            if (distanceToEnemy < shortestDistance)//if distance of this enemy is less then the shoetest Distance
            {
                //save the values of this enemy 
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)//if the nearest enemy is in range
        {
            target = nearestEnemy.transform;//set target as that enemy
        }
        else
        {
            target = null;//or let the target be empty
        }
    }

    private void Update()
    {
        //return if there is no target
        if (target == null)
        {
            return;
        }

        if (fireCooldown <= 0f)//ready to shoot 
        {
            Shoot();//call the shoot function
            fireCooldown = 1f / fireRate;//reset the firecooldown
        }
        fireCooldown -= Time.deltaTime;
    }
    //draw a circle of range size
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Shoot()
    {
        GameObject bulletGO;//temporary gameobject

        if (CheakQueue())//if the bullet queue is not empty
        {
            bulletGO = qbullet.Dequeue();//take the first bullet from the queue
            bulletGO.SetActive(true);//set it active
            bulletGO.transform.position = transform.position;//change the position to the turret position
        }
        else//if it is empty spawn a new bullet
        {
            bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
            bulletGO.transform.parent = this.transform;//set the turret as the bullet's parent
        }
        
        Arrow arrow = bulletGO.GetComponent<Arrow>();//creating an instance of the bullet script

        if (arrow != null)
        {
            arrow.seek(target,currentBulletSprite,damage);//giving target to the bullet
        }
    }

    //return if the queue is empty or not
    bool CheakQueue()
    {
        if (qbullet.Count > 0)
        {
            return true;
        }
        return false;

    }
}
