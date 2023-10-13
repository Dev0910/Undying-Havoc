using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image playerHealthBar;
    // Start is called before the first frame update
   /* void Start()
    {
        healthBar.fillAmount = PlayerController.currentHealth / GameManager.Instance.player.GetComponent<PlayerController>().startHealth;
    }*/

    // Update is called once per frame
    public void UpdatePlayerHP(float maxHP)
    {
        playerHealthBar.fillAmount = PlayerController.currentHealth / maxHP;
    }
}
