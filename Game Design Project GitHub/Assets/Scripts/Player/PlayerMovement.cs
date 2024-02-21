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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
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
        Vector2 lookDir = mousepos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }

    public void IncreaseMoveSpeed()
    {
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
[System.Serializable]
public class MoveSpeedLevels
{
    public string name;
    public float speed;
    public SingleResourse resourceToUpgrade;
}