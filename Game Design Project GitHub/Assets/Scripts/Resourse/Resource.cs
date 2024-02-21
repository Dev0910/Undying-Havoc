using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the gameobject resource
public class Resource : MonoBehaviour
{
    public ResourcesScriptableObject resource;
    public int amount;
    public float dropForce;
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = resource.sprite;
    }

    public void SetAmount(int _amount)
    {
        amount = _amount;
    }
    // to move it in a randon direction
    public void AddForce()
    {
        Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        gameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce,ForceMode2D.Impulse);
    }
    public void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = resource.sprite;
    }
}
