using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;

    public Color greenColor;
    public Color redColor;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(isOccupied==true)
        {
            rend.color = redColor;
        }
        else
        {
            rend.color = greenColor;
        }
    }

}
