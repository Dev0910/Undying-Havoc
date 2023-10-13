using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float startHealth;
    public static float currentHealth;  //playerHealth
    private UIManager uiManager;
    private void Start()
    {
        uiManager = GameManager.Instance.uiManager;
        currentHealth = startHealth;
        uiManager.UpdatePlayerHP(startHealth);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.Instance.dropAndCollectionManager.CollectGold(collision.gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        uiManager.UpdatePlayerHP(startHealth);
        if (currentHealth <= 0)
        {
            currentHealth -= damage;
            Debug.Log("Game Over");
            Time.timeScale = 0f;
        }
    }
}
