using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    //[Header("Partical Effect")]
    //[SerializeField] private GameObject deathEffect;

    [Header("Health")]
    [SerializeField] private List<PlayerMaxHealthLevels> maxHealthLevels;
    private int currentLevel;
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
    public static float currentOxygenLevel;

    private UIManager uiManager;
    [SerializeField]private float healthRegenrateRate;
    private PostProcessVolume postProcessVolume;
    private Vignette vignette;
    private OxygenGenerator oxygenGenerator;
    private bool isHPRegainRunning;

    
    private void Start()
    {
        Time.timeScale = 1f;
        //isInOxygenArea = true;
        currentLevel = 0;
        uiManager = GameManager.Instance.uiManager;
        currentMaxHealth = maxHealthLevels[0].maxHealth;
        currentHealth = currentMaxHealth;
        uiManager.UpdatePlayerHP(currentMaxHealth);
        oxygenGenerator = GameManager.Instance.oxygenGenerator;

        currentMaxOxygenCapacity = maxHealthLevels[0].maxHealth;
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
        isHPRegainRunning = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Resours"))
        {
            GameManager.Instance.dropAndCollectionManager.CollectResources(collision.gameObject);
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Oxygen Area"))
    //    {
    //        isInOxygenArea = false;
    //    }
    //}
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Oxygen Area"))
    //    {
    //        isInOxygenArea = true;
    //    }
    //}


    #region Health
    public void TakeDamage(float damage)
    {
        //AudioManager.Instance.PlaySFX("Player Damage 1");
        currentHealth -= damage;
        StopCoroutine(TakeDamageEffect());
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

    private IEnumerator RegenrateHealth()
    {
        yield return new WaitForSeconds(1.0f);
        if(!IsInOxygenArea())
        {
            yield return null;
        }
        if (currentHealth + (healthRegenrateRate * currentHealth*0.01f) <= currentMaxHealth)
        {
            currentHealth += healthRegenrateRate;
            uiManager.UpdatePlayerHP(currentMaxHealth);
            StartCoroutine(RegenrateHealth());
        }
        else
        {
            currentHealth = currentMaxHealth;
            uiManager.UpdatePlayerHP(currentMaxHealth);
            isHPRegainRunning = false;
        }
        
        
    }

    public void IncreaseMaxHealth()
    {
        if (GameManager.Instance.gameStats.CheakIfResourseAvailable(maxHealthLevels[currentLevel].resourseToUpgrade.resource, maxHealthLevels[currentLevel].resourseToUpgrade.amount) && currentLevel <= maxHealthLevels.Count)
        {
            GameManager.Instance.gameStats.RemoveResourse(maxHealthLevels[currentLevel].resourseToUpgrade.resource, maxHealthLevels[currentLevel].resourseToUpgrade.amount);
            currentLevel++;
            currentMaxHealth = maxHealthLevels[currentLevel].maxHealth;
            currentHealth = currentMaxHealth;
            if (maxHealthLevels[currentLevel].resourseToUpgrade.resource != EResources.None)
            {
                uiManager.UpdatePlayerMaxHealth(maxHealthLevels[currentLevel+1].maxHealth, maxHealthLevels[currentLevel].resourseToUpgrade.resource, maxHealthLevels[currentLevel].resourseToUpgrade.amount);
            }
            uiManager.UpdatePlayerHP((int)currentHealth);
            
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

        bool isInOxygenArea = IsInOxygenArea();
        if (isInOxygenArea && currentOxygenLevel<currentMaxOxygenCapacity)
        {
            currentOxygenLevel += (currentMaxOxygenCapacity * oxygenRegainRate) / 100;
            currentOxygenLevel = currentOxygenLevel >= currentMaxOxygenCapacity?currentMaxOxygenCapacity:currentOxygenLevel;
        }
        else if(isInOxygenArea && currentOxygenLevel == currentMaxOxygenCapacity && currentHealth<currentMaxHealth && !isHPRegainRunning)
        {
            StartCoroutine(RegenrateHealth());
            isHPRegainRunning = true;
        }
        else if(!isInOxygenArea)
        {
            if(isHPRegainRunning)
            {
                StopCoroutine(RegenrateHealth());
                isHPRegainRunning= false;
            }
            currentOxygenLevel -= (currentMaxOxygenCapacity * oxygenDeplitionRate) / 100;
            if(currentOxygenLevel <= 0)
            {
                TakeDamage(damageWhenOxygenZero);
            }
        }
        GameManager.Instance.uiManager.UpdateOxygenBar(currentMaxOxygenCapacity);
    }

    private bool IsInOxygenArea()
    {
        bool result = false;
        float distance = Vector2.Distance(this.transform.position, oxygenGenerator.gameObject.transform.position)/2;
        if(distance <= oxygenGenerator.range+(0.045*oxygenGenerator.range) && oxygenGenerator.isOxygenAreaUp)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        

        return result;
    }
    #endregion
}
[System.Serializable]
public class PlayerMaxHealthLevels
{
    public string name;
    public int maxHealth;
    public SingleResourse resourseToUpgrade;
}