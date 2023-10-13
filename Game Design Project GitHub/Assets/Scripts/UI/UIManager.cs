using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image playerHealthBar;
    public void UpdatePlayerHP(float maxHP)
    {
        playerHealthBar.fillAmount = PlayerController.currentHealth / maxHP;
    }
}
