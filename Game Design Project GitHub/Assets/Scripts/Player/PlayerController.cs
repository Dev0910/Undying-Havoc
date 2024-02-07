using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //[Header("Partical Effect")]
    //[SerializeField] private GameObject deathEffect;

    [Header("Health")]
    [SerializeField] private int startMaxHealth; 
    [SerializeField] private int costToUpgradeMaxHealth;
    private int currentCostToIncreaseMaxHealth;
    private int currentMaxHealth;
    public static float currentHealth;//playerHealth

    [Header("Oxygen")]
    [SerializeField] private Image OxygenBar;
    [SerializeField] private float startMaxOxygenCapacity;
    [SerializeField] private int costToUpgradeMaxOxygenCapacity;
    [SerializeField] private float oxygenDeplitionRate;
    [SerializeField] private float oxygenRegainRate;
    [SerializeField] private float damageWhenOxygenZero;
    
    private int currentCostToIncreaseMaxOxygenCapacity;
    private int currentMaxOxygenCapacity;
    private bool isInOxygenArea;
    public static float currentOxygenLevel;

    private UIManager uiManager;
    private float regenrateRate;
    private PostProcessVolume postProcessVolume;
    private Vignette vignette;


    
    private void Start()
    {
        Time.timeScale = 1f;
        isInOxygenArea = true;
        uiManager = GameManager.Instance.uiManager;
        currentMaxHealth = startMaxHealth;
        currentHealth = currentMaxHealth;
        currentCostToIncreaseMaxHealth = costToUpgradeMaxHealth;
        uiManager.UpdatePlayerHP(currentMaxHealth);

        currentMaxOxygenCapacity = startMaxHealth;
        currentOxygenLevel = currentMaxOxygenCapacity;

        postProcessVolume = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings<Vignette>(out vignette);
        if(!vignette)
        {
            print("error : vigette empty");
        }
        else
        {
            vignette.enabled.Override(false);
        }
        InvokeRepeating("OxygenCheak", 0.5f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Resours"))
        {
            GameManager.Instance.dropAndCollectionManager.CollectResources(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Oxygen Area"))
        {
            isInOxygenArea = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Oxygen Area"))
        {
            isInOxygenArea = true;
        }
    }


    #region Health
    public void TakeDamage(float damage)
    {
        //AudioManager.Instance.PlaySFX("Player Damage 1");
        currentHealth -= damage;
        StartCoroutine(TakeDamageEffect());
        uiManager.UpdatePlayerHP(currentMaxHealth);
        if (currentHealth <= 0)
        {
            currentHealth -= damage;
            Debug.Log("Game Over");
            //GameObject e = Instantiate(deathEffect);
            //Destroy(e,10f);
            Time.timeScale = 0f;
            GameManager.Instance.mainScreenUI.LoadScene("EndScene");
            //Invoke(nameof(ShowEndScreen), 1f);

        }
    }

    public void RegenrateHealth(float time)
    {
        float missingHealth = currentMaxHealth-currentHealth;
        regenrateRate = (missingHealth / (time * 0.8f))/2;
        if(regenrateRate > 0)
        {
            InvokeRepeating(nameof(GetHealth), 0f, 0.5f);
        }
    }

    //called every 0.5sec by the RegenrateHealth to regenrate the missing health
    private void GetHealth()
    {
        if(GameManager.Instance.dayAndNight.isNight)
        {
            CancelInvoke();
            return;
        }


        if(currentHealth + regenrateRate <= currentMaxHealth)
        {
            currentHealth += regenrateRate;
        }
        else
        {
            currentHealth = currentMaxHealth;
            CancelInvoke();
        }
        uiManager.UpdatePlayerHP(currentMaxHealth);
    }


    public void IncreaseMaxHealth()
    {
        if(GameStats.currentGold>=currentCostToIncreaseMaxHealth)
        {
            GameStats.currentGold -= currentCostToIncreaseMaxHealth;
            currentMaxHealth += 100;
            currentHealth = currentMaxHealth;
            currentCostToIncreaseMaxHealth += currentCostToIncreaseMaxHealth;
            uiManager.UpdatePlayerMaxHealth(currentMaxHealth, currentCostToIncreaseMaxHealth);
            uiManager.UpdatePlayerHP(currentMaxHealth);
        }
        
    }

    private IEnumerator TakeDamageEffect()
    {
        float intensity = 0.4f;

        vignette.enabled.Override(true);
        vignette.intensity.Override(intensity);
        yield return new WaitForSeconds(0.4f);


        while(intensity>0)
        {
            intensity -= 0.01f;

            intensity = intensity < 0 ? 0 : intensity;

            vignette.intensity.Override(intensity);

            yield return new WaitForSeconds(0.1f);
        }
        vignette.enabled.Override(false);
        yield break;
    }
    #endregion

    #region Oxygen
    private void OxygenCheak()
    {
        if(isInOxygenArea && currentOxygenLevel<currentMaxOxygenCapacity)
        {
            currentOxygenLevel += (currentMaxOxygenCapacity * oxygenRegainRate) / 100;
            currentOxygenLevel = currentOxygenLevel >= currentMaxOxygenCapacity?currentMaxOxygenCapacity:currentOxygenLevel;
        }
        else if(!isInOxygenArea)
        {
            currentOxygenLevel -= (currentMaxOxygenCapacity * oxygenDeplitionRate) / 100;
            if(currentOxygenLevel <= 0)
            {
                TakeDamage(damageWhenOxygenZero);
            }
        }
        GameManager.Instance.uiManager.UpdateOxygenBar(currentMaxOxygenCapacity);
    }
    #endregion
}
