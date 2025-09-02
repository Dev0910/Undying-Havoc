using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]private Vector3 ofSet;
    private Vector3 playerPosition;
    // Update is called once per frame
    void LateUpdate()
    {
        playerPosition = GameManager.Instance.player.transform.position + ofSet;
        this.transform.position = playerPosition;
    }
}
