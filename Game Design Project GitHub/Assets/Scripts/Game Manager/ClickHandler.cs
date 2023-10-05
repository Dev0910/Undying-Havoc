using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public static bool xDown;
    public static bool vDown;
    
    void Update()
    {
        //cheak for X input
        if (Input.GetKey(KeyCode.X)) { xDown = true; } else if (!Input.GetKey(KeyCode.X)) { xDown = false; }
        //cheak for V input
        if (Input.GetKey(KeyCode.V)) { vDown = true; } else if (!Input.GetKey(KeyCode.V)) { vDown = false; }
    }

}
