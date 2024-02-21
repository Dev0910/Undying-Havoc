using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    public List<MoveSpeedLevels> moveSpeedLevels;
    private float currentMoveSpeed;
    private int currentLevel;
    private float moveSpeed;
    public Rigidbody2D rb;
    public Camera cam;
    //public GameObject target;
    Vector2 movement;
    Vector2 mousepos;

    private float rotationAngle = 45.0f;
    private void Start()
    {
        currentLevel = 0;
        currentMoveSpeed = moveSpeedLevels[currentLevel].speed;
        
    }
    // Update is called once per frame
    void Update()
    {
        //stores the input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //normalize the input
        movement = movement.normalized;
        //stores the mouse position
        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void LateUpdate()
    { 
        // called on mouse clicked
        if (Input.GetMouseButtonDown(0))
        {
            transform.Rotate(Vector3.forward * rotationAngle);
        }
        
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentMoveSpeed * Time.fixedDeltaTime);//moves the player

        //set the player rotation accoring to the mouse posotion
        Vector2 lookDir = mousepos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }

    //called by the buttons in the shop to increase the move speed
    public void IncreaseMoveSpeed()
    {
        //cheak if the resource required to upgrade are there of not
        if (GameManager.Instance.gameStats.CheakIfResourseAvailable(moveSpeedLevels[currentLevel].resourceToUpgrade.resource, moveSpeedLevels[currentLevel].resourceToUpgrade.amount) && currentLevel<=moveSpeedLevels.Count)
        {
            currentLevel++;
            GameManager.Instance.gameStats.RemoveResourse(moveSpeedLevels[currentLevel].resourceToUpgrade.resource, moveSpeedLevels[currentLevel].resourceToUpgrade.amount);
            currentMoveSpeed = moveSpeedLevels[currentLevel].speed;
            if (moveSpeedLevels[currentLevel].resourceToUpgrade.resource!=EResources.None)
            {
                GameManager.Instance.uiManager.UpdatePlayerMoveSpeed(moveSpeedLevels[currentLevel+1].speed, moveSpeedLevels[currentLevel].resourceToUpgrade.resource, moveSpeedLevels[currentLevel].resourceToUpgrade.amount);
            }
        }
    }
}

//a class to store all the levels of the move speed
[System.Serializable]
public class MoveSpeedLevels
{
    public string name;
    public float speed;
    public SingleResourse resourceToUpgrade;
}