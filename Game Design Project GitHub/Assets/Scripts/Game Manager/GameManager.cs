using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GridSystem gridSystem;
    public SpawnMannager spawnManager;
    public DayAndNight dayAndNight;
    public EnemyScriptableObject enemyScriptableObject;
    public DropAndCollectionManager dropAndCollectionManager;
    public GameStats gameStats;
    public UIManager uiManager;
    public MainScreenUi mainScreen;
    
    public GameObject player;
    public GameObject weaponHolder;

    
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }


        dayAndNight = GetComponentInChildren<DayAndNight>();
        gridSystem = GetComponentInChildren<GridSystem>();
        spawnManager = GetComponentInChildren<SpawnMannager>();
        dropAndCollectionManager = GetComponentInChildren<DropAndCollectionManager>();
        gameStats = GetComponentInChildren<GameStats>();
        uiManager = GetComponentInChildren<UIManager>();
        mainScreen = GameObject.Find("Canvas").GetComponent<MainScreenUi>();

        player = GameObject.FindGameObjectWithTag("Player");
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
    }
}
