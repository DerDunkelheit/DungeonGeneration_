using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dungeon manager takes info from this class to manage gameConfigs on each Level
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public bool isFirstLevel = true;
    public int currentLevelID;

    [Header("Enemies count")]
    [SerializeField] int currentEnemiesCount = 0;

    [Header("Additional Dungeon Settings")]
    [SerializeField] int minFloorToAddToNextLevel = 50;
    [SerializeField] int maxFloorToAddToNextLevel = 75;

    [Header("Boos Fields")]
    [SerializeField] int maxLevelOnThisStage = 6;
    public int boosRoomWidth = 8;
    public int boosRoomHeight = 8;


    public int FloorAmount { get;private set; }

    DungeonManager dungeonManager;
   [HideInInspector]public DungeonType dungeonType;

    public int CurrentEnemiesCount => currentEnemiesCount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        dungeonManager = GameObject.FindGameObjectWithTag("Dungeon Manager").GetComponent<DungeonManager>();

        FloorAmount = 0;
        currentLevelID = 0;

        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// This method is used to manage the current dungeon setting based on the level ID.
    /// </summary>
    public void ManageDungeonType() //TODO: make a good random dungeon Variation
    {
        if(currentLevelID > 0)
        {
            isFirstLevel = false;
        }

        if(currentLevelID == 2) // Sets dungeon type to rooms at 3 level
        {
            dungeonType = DungeonType.Rooms;
        }

        if(currentLevelID >2)
        {
            dungeonType = DungeonType.Cavern;
        }

        if(currentLevelID == maxLevelOnThisStage)
        {
            dungeonType = DungeonType.BoosRoom;
        }

    }

    /// <summary>
    /// this method is used to find a new dungeon Manager, because each scene has its own Manager, and the level Manager has the DontDestroyOnLoad function
    /// </summary>
    public void FindNewDungeonManagerObject()
    {
        dungeonManager = GameObject.FindGameObjectWithTag("Dungeon Manager").GetComponent<DungeonManager>();
    }

    public void UpdateEnemyCountOnNewLevel(int amount)
    {
        currentEnemiesCount = amount;
    }

    public void ReduceEnemiesCountVariable()
    {
        currentEnemiesCount--;
    }

    public void UpdateAdditionalFloorCount()
    {
        int ammountToAdd = Random.Range(minFloorToAddToNextLevel, maxFloorToAddToNextLevel + 1);
        FloorAmount += ammountToAdd;
    }

    
   
}
