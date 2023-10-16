using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    //public GameObject target;
    Vector2 movement;
    Vector2 mousepos;

    /*private Quaternion initialRotation;
    private Quaternion targetRotation;
    private float rotationSpeed = 90.0f; // Degrees per second
    private float returnSpeed = 90.0f; // Degrees per second
    private bool isRotating;*/

    private void Start()
    {
        /*initialRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, 90, 0);*/
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
       // Animation();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //if (ScreenMannager.isPause) return;
        ////Movement 
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        ////rotating the head
        //if (Shooting.target == null) { return; }
        Vector2 lookDir = mousepos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }
    /*void Animation()
    {
        if (isRotating)
        {
            // Rotate towards the targetRotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if we have reached the targetRotation
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
            }
        }
        else
        {
            // Return to the initialRotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, returnSpeed * Time.deltaTime);
        }
    }*/

/*
    private void OnMouseDown()
    {
        isRotating = true;
    }*/
}
