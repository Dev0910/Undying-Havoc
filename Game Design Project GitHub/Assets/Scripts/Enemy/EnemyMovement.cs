using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = player.transform.position - this.transform.position;
        transform.Translate(dir * Time.deltaTime * Speed);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
    }
}
