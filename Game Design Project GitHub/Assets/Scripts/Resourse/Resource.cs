using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the gameobject resource
public class Resource : MonoBehaviour
{
    public ResourcesScriptableObject resource;
    public Collider2D collider2D;
    public int amount;
    public float dropForce;
    public float animationTime;
    public float scale;
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = resource.sprite;
        
    }
    public void SetAmount(int _amount)
    {
        amount = _amount;
    }

    public void ResetResource()
    {
        //move in a random direction
        Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        gameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        //change sprite of the resources
        gameObject.GetComponent<SpriteRenderer>().sprite = resource.sprite;
        
    }
    IEnumerator StartAnimation()
    {
        collider2D.enabled = false;
        for (int i = 50; i <= 100; i++)
        {
            yield return new WaitForSeconds(animationTime / 100);
            transform.localScale = Vector3.one * (i * 0.01f) * scale;
        }
        collider2D.enabled = true;
    }

    private void OnEnable()
    {
        StartCoroutine(StartAnimation());
    }

    private void OnDisable()
    {
        StopCoroutine(StartAnimation());
    }
}
