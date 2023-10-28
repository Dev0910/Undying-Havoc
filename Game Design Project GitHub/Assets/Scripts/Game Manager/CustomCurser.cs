using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCurser : MonoBehaviour
{
    public Sprite defaultSprite;
    private void Start()
    {
        //this.gameObject.SetActive(true);
        //GetComponent<SpriteRenderer>().sprite = defaultSprite;

    }

    // Update is called once per frame
    void Update()
    {
        //if(Cursor.visible)
        //{
        //    Cursor.visible = false;
        //}
        //get the mouse position in Vector2/(x,y)
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //set the transform to the mouse position 
        transform.position = mousePosition;
    }
}
