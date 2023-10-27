using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField]private Image playerHealthBar;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI playerMaxhealth;
    [SerializeField] private TextMeshProUGUI costToUpgradeMaxHelth;

    [Header("Player MoveSpeed")]
    [SerializeField] private TextMeshProUGUI playerMoveSpeed;
    [SerializeField] private TextMeshProUGUI costToUpgradeMoveSpeedText;
    public void UpdatePlayerHP(float maxHP)
    {
        playerHealthBar.fillAmount = PlayerController.currentHealth / maxHP;
        playerHealthText.text = PlayerController.currentHealth + " / " + maxHP;
    }
    public void UpdatePlayerMaxHealth(int maxHealth,int cost)
    {
        playerMaxhealth.text ="Max Health : " + (maxHealth+100).ToString();
        costToUpgradeMaxHelth.text = "Cost : " + cost.ToString();
    }
    public void UpdatePlayerMoveSpeed(float moveSpeed,float cost) 
    {
        playerMoveSpeed.text = "Player Movement Speed : "+moveSpeed.ToString();
        costToUpgradeMoveSpeedText.text = "Cost : " + cost.ToString();
    }
}
