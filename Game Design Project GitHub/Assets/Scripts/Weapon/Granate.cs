using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//not used in the current game 
public class Granate : MonoBehaviour
{
    private Vector2 targetPos;
    public float damage;
    public float attackrange = 0.4f;
    public float speed = 5;
    public Sprite[] sprites;
    private int currentSpriteIndex;
    private SpriteRenderer spriteRenderer;
    public LayerMask enemylayers;

    public void GetDamage(float _damage)
    {
        damage = _damage;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpriteIndex = 0;
        spriteRenderer.sprite = sprites[currentSpriteIndex];
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Update()
    {
        if(speed == 0) { return; }

        if(speed > 0)
        {
            speed -= Random.Range(0.1f, 0.25f);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
        }
        else
        {
            speed = 0;
            StartCoroutine(Animation());
            Invoke("Blast", 1.3f);
        }
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(1);
        while(currentSpriteIndex < sprites.Length)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            currentSpriteIndex++;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);

    }

    void Blast()
    {
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(transform.position, attackrange, enemylayers);

        for (int i = 0; i < hitenemies.Length; i++)
        {
            hitenemies[i].gameObject.GetComponent<BaseEnemy>().TakeDamage(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }
}
