using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    //public GameObject target;
    Vector2 movement;
    Vector2 mousepos;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private float rotationSpeed = 40.0f; // Degrees per second
    private bool isRotating = false;
    public Transform target;
    public Transform temporary;
    private float rotationAngle = 45.0f;

    private void Start()
    {
        initialRotation = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //targetRotation = Quaternion.Euler(0, 0, -90) * Quaternion.Inverse(targetRotation);
            //isRotating = true;
            transform.Rotate(Vector3.forward * rotationAngle);
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
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
}
