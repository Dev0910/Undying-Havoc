using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static EnemyData[] enemyDatas;
    public static GridSystem gridSystem;
    public SpawnMannager spawnMannager;
    public EnemyScriptableObject enemyScriptableObject;

    private GameObject buildingToPlace;
    public GameObject player;
    
    public int currentWave;
    public bool isNight = false;
    public float timeBetweenDayAndNight = 15f;
    
    
    public CustomCurser customCurser;
    // Start is called before the first frame update
    void Start()
    {
        gridSystem = GetComponent<GridSystem>();
        spawnMannager = GetComponent<SpawnMannager>();

        player = GameObject.FindGameObjectWithTag("Player");

        enemyDatas = enemyScriptableObject.enemyData;
        
        currentWave = 0;
        isNight = false;
    }

    private void Update()
    {
        //place the building on left mouse click
        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
        {
            //get the nearest tile by calling the get nearest tile function in grid system
            GameObject nearestTile = gridSystem.GetNearestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //cheak the comditions to place a building
            if (nearestTile != null && nearestTile.GetComponent<Tile>().isOccupied == false && Stats.currentGold >= buildingToPlace.GetComponent<BuildingBuleprint>().cost)
            {
                Stats.currentGold -= buildingToPlace.GetComponent<BuildingBuleprint>().cost;//take the cost of building
                Instantiate(buildingToPlace, nearestTile.transform.position, Quaternion.identity);//spawn the building
                nearestTile.GetComponent<Tile>().isOccupied = true;//make the isOccupied tile true

                //if you don't have the mony to buy the next building
                if(Stats.currentGold - buildingToPlace.GetComponent<BuildingBuleprint>().cost < 0)
                {
                    DeselectBuilding();
                }
            }
            
        }
        //deselect the Building on Right mouse click
        if(Input.GetMouseButtonDown(1))
        {
            DeselectBuilding();
        }
        
    }

    //set the building to be bought on button click
    //this function is called by the buttons in UI
    public void BuyBuilding(GameObject building)
    {
        //cheaks the conditions to buy the building
        if(Stats.currentGold >=building.GetComponent<BuildingBuleprint>().cost)
        {
            customCurser.gameObject.SetActive(true);
            customCurser.GetComponent<SpriteRenderer>().sprite = building.GetComponent<BuildingBuleprint>().spriteTOBuild;//sets the curser to the sprite of the building
            Cursor.visible = false;

            
            buildingToPlace = building;//set the buildingToPlace to this building
            gameObject.GetComponent<GridSystem>().GetGrid();//draws the grid
        }
    }

    //buys weapon for the player
    //this function is called by the buttons in UI
    public void BuyWeapon(GameObject prefab)
    {
        //cheaks the conditions to buy the weapon
        if (prefab.GetComponent<PlayerCombat>().costToBuy <= Stats.currentGold) 
        {
            Stats.currentGold -= prefab.GetComponent<PlayerCombat>().costToBuy;//reduse the weapon cost
            GameObject instantiatedPrefab = Instantiate(prefab, player.transform.position, player.transform.rotation);//spawns the weapon
            instantiatedPrefab.transform.parent = player.transform;//set player as the parent gameobject
        }
    }

    //Deselect the Building from the curser
    private void DeselectBuilding()
    {
        buildingToPlace = null;
        customCurser.gameObject.SetActive(false);
        Cursor.visible = true;
        gameObject.GetComponent<GridSystem>().RemoveGrid();
    }

}
