using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    public float startMoveSpeed = 5f;
    public float increaseMoveSpeedBy = 1f;
    public float costToIncreaseMoveSpeed = 200;
    private float currentCostToIncreaseMoveSpeed;
    private float currentMoveSpeed;
    public Rigidbody2D rb;
    public Camera cam;
    //public GameObject target;
    Vector2 movement;
    Vector2 mousepos;

    private float rotationAngle = 45.0f;
    private void Start()
    {
        currentMoveSpeed = startMoveSpeed;
        currentCostToIncreaseMoveSpeed = costToIncreaseMoveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void LateUpdate()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            transform.Rotate(Vector3.forward* rotationAngle);
        }
        
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentMoveSpeed * Time.fixedDeltaTime);
        //if (ScreenMannager.isPause) return;
        ////Movement 
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        ////rotating the head
        //if (Shooting.target == null) { return; }
        Vector2 lookDir = mousepos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }

    public void IncreaseMoveSpeed()
    {
        
        if (GameStats.currentGold >= currentCostToIncreaseMoveSpeed)
        {
            GameStats.currentGold -= Mathf.RoundToInt(currentCostToIncreaseMoveSpeed);
            currentMoveSpeed += increaseMoveSpeedBy;
            currentCostToIncreaseMoveSpeed += currentCostToIncreaseMoveSpeed;
            GameManager.Instance.uiManager.UpdatePlayerMoveSpeed(currentMoveSpeed, currentCostToIncreaseMoveSpeed);
        }
    }
}
