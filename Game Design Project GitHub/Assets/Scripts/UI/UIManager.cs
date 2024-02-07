using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float lerpAmount;
    [Header("Player Health")]
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI playerMaxhealth;
    [SerializeField] private TextMeshProUGUI costToUpgradeMaxHelth;

    [Header("Oxygen")]
    [SerializeField] private Image oxygenBar;


    [Header("Player MoveSpeed")]
    [SerializeField] private TextMeshProUGUI playerMoveSpeed;
    [SerializeField] private TextMeshProUGUI costToUpgradeMoveSpeedText;

    
    public void UpdatePlayerHP(float maxHP)
    {
        playerHealthBar.fillAmount = PlayerController.currentHealth / maxHP;
        playerHealthText.text = Mathf.RoundToInt(PlayerController.currentHealth) + " / " + maxHP;
    }
    public void UpdatePlayerMaxHealth(int maxHealth,int cost)
    {
        playerMaxhealth.text ="Max Health : " + (maxHealth+100).ToString();
        costToUpgradeMaxHelth.text = "Cost : " + cost.ToString();
    }
    public void UpdatePlayerMoveSpeed(float moveSpeed,float cost) 
    {
        playerMoveSpeed.text = "Movement Speed : "+moveSpeed.ToString();
        costToUpgradeMoveSpeedText.text = "Cost : " + cost.ToString();
    }
    public void UpdateOxygenBar(float maxCapacity)
    {
        float currentvelocity = 0.0f;
        float temp = Mathf.SmoothDamp(oxygenBar.fillAmount, PlayerController.currentOxygenLevel / maxCapacity,ref currentvelocity, 100 * Time.deltaTime);
        float temp2 = Mathf.Lerp(oxygenBar.fillAmount, PlayerController.currentOxygenLevel / maxCapacity, lerpAmount);
        oxygenBar.fillAmount = temp2;
        //playerHealthText.text = Mathf.RoundToInt(PlayerController.currentHealth) + " / " + maxCapacity;
    }
}
