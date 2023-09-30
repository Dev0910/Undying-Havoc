using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    private void Update()
    {
        FollowTarget(Player.transform.position);
    }
}
