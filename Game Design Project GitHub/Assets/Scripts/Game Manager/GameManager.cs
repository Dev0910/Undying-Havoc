using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnemyData[] enemyDatas;

    public GridSystem gridSystem;
    public SpawnMannager spawnManager;
    public DayAndNight dayAndNight;
    public EnemyScriptableObject enemyScriptableObject;
    public DropAndCollectionManager dropAndCollectionManager;
    public GameStats gameStats;
    public UIManager uiManager;
    
    public GameObject player;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

        player = GameObject.FindGameObjectWithTag("Player");
        enemyDatas = enemyScriptableObject.enemyData;
    }
}
