using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCurser : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //get the mouse position in Vector2/(x,y)
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //set the transform to the mouse position 
        transform.position = mousePosition;
    }
}
