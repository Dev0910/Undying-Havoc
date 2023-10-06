using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            GameManager.Instance.dropAndCollectionManager.CollectGold(other.gameObject);
        }
    }
}
