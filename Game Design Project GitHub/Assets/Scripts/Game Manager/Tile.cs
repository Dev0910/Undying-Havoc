using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;

    public Color greenColor;
    public Color redColor;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //change the color of the tile according according to the status of the tile
        if(isOccupied==true)
        {
            spriteRenderer.color = redColor;
        }
        else
        {
            spriteRenderer.color = greenColor;
        }
    }

}
