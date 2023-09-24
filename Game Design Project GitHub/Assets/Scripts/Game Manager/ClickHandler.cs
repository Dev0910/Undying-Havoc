using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public static bool xDown;
    public static bool vDown;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X)) { xDown = true; } else if (!Input.GetKey(KeyCode.X)) { xDown = false; }
        if (Input.GetKey(KeyCode.V)) { vDown = true; } else if (!Input.GetKey(KeyCode.V)) { vDown = false; }
    }

}
