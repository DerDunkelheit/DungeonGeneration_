using System;
using System.Collections;
using System.Collections.Generic;
using AStart;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine;

public enum DungeonType
{
    Cavern,
    Rooms,
    Winding,
    BoosRoom
};

public class DungeonManager : MonoBehaviour
{

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    [Header("Dungeon Type")]
    public DungeonType dungeonType;
    [Tooltip("Only if dungeonType is Winding")] [Range(0, 100)] public int windingHollPercent;

    [Header("General Settings")]
    public int totalFloorCount;
    public GameObject exitPrefab;
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject tilePrefab;

    [Header("Edges Settings")]
    public bool useRoundedEdges = false;
    public GameObject[] roundedEdges;

    [Header("Items Settings")]
    [Range(0, 100)] public int itemSpawnPercent;
    public GameObject[] interactiveItems = null;
    public Count interactiveItemsCount = new Count(3, 4);
    public GameObject[] staticItems = null;
    public Count staticItemsCount = new Count(1, 3);

    [Header("Enemies Settings")]
    [SerializeField] Transform enemiesHolder = null;
    public GameObject[] randomEnemies;
    public Count enemiesCount = new Count(1, 4);

    [HideInInspector] public float minX, maxX, minY, maxY;

    List<Vector3> floorList = new List<Vector3>();
    List<Vector3> floorPosition = new List<Vector3>(); //A list of floorPositions.
    LayerMask floorMask, wallMask;


    Transform playerTrans; //Field for Debbug.

    AStarManager aStarManager;

    LevelManager levelManager;

    int amountOfEnemiesOnLevel;

    void Awake()
    {
     
        playerTrans = GameObject.FindWithTag("Player").GetComponent<Transform>();
        aStarManager = this.GetComponent<AStarManager>();

        levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();

        totalFloorCount += levelManager.FloorAmount;

        amountOfEnemiesOnLevel = 0;
    }

    void Start()
    {
        if (levelManager.isFirstLevel)
        {

        }
        else
        {
            dungeonType = levelManager.dungeonType;
        }

        floorMask = LayerMask.GetMask("Floor");
        wallMask = LayerMask.GetMask("Wall");

        floorPosition.Clear(); //cleat list,before  a new initialization one

        switch (dungeonType)
        {
            case DungeonType.Cavern:
                RandomWalker();
                break;

            case DungeonType.Rooms:
                RoomWalker();
                break;

            case DungeonType.Winding:
                WindingWalker();
                break;

            case DungeonType.BoosRoom:
                SpawnBoosRoom();
                break;
        }

       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && Application.isEditor)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            playerTrans.position = Vector3.zero; //Set player Pos to vector3.zero in new level
        }
    }

    private void RandomWalker()
    {
        Vector3 curPos = Vector3.zero;
        floorList.Add(curPos);

        while (floorList.Count < totalFloorCount)
        {
            curPos += GenerateRandomDirection();

            if (!InFloorList(curPos))
            {
                floorList.Add(curPos);
            }
        }

       
        StartCoroutine(DelayProgress());

    }

    private void RoomWalker()
    {
        Vector3 curPos = Vector3.zero;
        floorList.Add(curPos);

        while (floorList.Count < totalFloorCount)
        {
            Vector3 walkDir = GenerateRandomDirection();
            int walkLength = Random.Range(9, 18);

            for (int i = 0; i < walkLength; i++)
            {
                if (!InFloorList(curPos + walkDir))
                {
                    floorList.Add(curPos + walkDir);
                }

                curPos += walkDir;
            }

            RandomRoom(curPos, 1, 5);
        }

        StartCoroutine(DelayProgress());
    }

    private void WindingWalker()
    {
        Vector3 curPos = Vector3.zero;
        floorList.Add(curPos);

        while (floorList.Count < totalFloorCount)
        {
            int roll = Random.Range(0, 100);

            Vector3 walkDir = GenerateRandomDirection();
            int walkLength = Random.Range(9, 18);

            for (int i = 0; i < walkLength; i++)
            {
                if (!InFloorList(curPos + walkDir))
                {
                    floorList.Add(curPos + walkDir);
                }

                curPos += walkDir;
            }

            if (roll > windingHollPercent)
            {
                RandomRoom(curPos, 1, 5);
            }

        }

        StartCoroutine(DelayProgress());
    }

    private void SpawnBoosRoom()
    {
        Vector3 curPos = Vector3.zero;
        floorList.Add(curPos);

        for (int x = 0; x < levelManager.boosRoomHeight; x++)
        {
            for (int y = 0; y < levelManager.boosRoomWidth; y++)
            {
                curPos = new Vector3(x, y);
                floorList.Add(curPos);
            }
        }

        StartCoroutine(DelayProgress(true));
    }

    private void RandomRoom(Vector3 currentPosition, int randomMin, int randomMax)
    {
        int roomWidth = Random.Range(randomMin, randomMax);
        int roomHeight = Random.Range(randomMin, randomMax);

        for (int w = -roomWidth; w <= roomWidth; w++)
        {
            for (int h = -roomHeight; h <= roomHeight; h++)
            {
                Vector3 offset = new Vector3(w, h, 0);

                if (!InFloorList(currentPosition + offset))
                {
                    floorList.Add(currentPosition + offset);
                }
            }
        }
    }

    private Vector3 GenerateRandomDirection()
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                return Vector3.up;

            case 2:
                return Vector3.right;

            case 3:
                return Vector3.down;

            case 4:
                return Vector3.left;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// Checking if a random position is already in the list of floors
    /// </summary>
    /// <param name="curPosition"></param>
    /// <returns></returns>
    private bool InFloorList(Vector3 curPosition)
    {
        for (int i = 0; i < floorList.Count; i++)
        {
            if (Vector3.Equals(curPosition, floorList[i]))
            {
                return true;
            }
        }

        return false;
    }

    private void SpawnExit()
    {
        int randomExitPos = Random.Range(1, 4);

        Vector3 doorPos = floorList[floorList.Count - randomExitPos];

        GameObject goExit = Instantiate(exitPrefab, doorPos, Quaternion.identity);
        goExit.name = exitPrefab.name;
        goExit.transform.SetParent(this.transform);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arrayOfItems"></param>
    /// <param name="minimum">minimumAmountPerLevel</param>
    /// <param name="maximum">maximumAmountPerLevel</param>
    private void SpawnObjects(GameObject[] arrayOfItems, int minimum, int maximum)
    {
        int itemsCount = Random.Range(minimum, maximum + 1);

        for (int x = (int)minX - 2; x <= (int)maxX; x++)
        {
            for (int y = (int)minY - 2; y <= (int)maxY; y++)
            {
                Collider2D hitFloor = Physics2D.OverlapBox(new Vector2(x, y), Vector2.one * 0.8f, 0, floorMask);

                if (itemsCount == 0) { return; }

                if (hitFloor)
                {
                    if (!Vector2.Equals(hitFloor.transform.position, floorList[floorList.Count - 1]))
                    {
                        Collider2D hitTop = Physics2D.OverlapBox(new Vector2(x, y + 1), Vector2.one * 0.8f, 0, wallMask);
                        Collider2D hitRight = Physics2D.OverlapBox(new Vector2(x + 1, y), Vector2.one * 0.8f, 0, wallMask);
                        Collider2D hitLeft = Physics2D.OverlapBox(new Vector2(x - 1, y), Vector2.one * 0.8f, 0, wallMask);
                        Collider2D hitBottom = Physics2D.OverlapBox(new Vector2(x, y - 1), Vector2.one * 0.8f, 0, wallMask);

                        if (RandomItems(arrayOfItems, hitFloor, hitTop, hitRight, hitBottom, hitLeft))
                        {
                            itemsCount--;
                        }

                    }
                }
            }
        }
    }

    private bool RandomItems(GameObject[] Items, Collider2D hitFloor, Collider2D hitTop, Collider2D hitRight, Collider2D hitBottom, Collider2D hitLeft)
    {
        if ((hitTop || hitRight || hitBottom || hitLeft) && !(hitTop && hitBottom) && !(hitLeft && hitRight))
        {
            int roll = Random.Range(0, 101);
            if (roll <= itemSpawnPercent)
            {
                int itemIndex = Random.Range(0, Items.Length);
                GameObject goItem = Instantiate(Items[itemIndex], hitFloor.transform.position, Quaternion.identity);
                goItem.name = Items[itemIndex].name;
                goItem.transform.SetParent(hitFloor.transform);

                return true;
            }
        }

        return false;
    }

    private void SpawnEnemies(GameObject[] arraysOfEnemy, int minimum, int maximum)
    {
        int enemiesCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < enemiesCount; i++)
        {
            Vector3 randomPos = randomPosition();
            int randomEnemyIndex = Random.Range(0, arraysOfEnemy.Length);

            GameObject goEnemy = Instantiate(arraysOfEnemy[randomEnemyIndex], randomPos, Quaternion.identity);
            goEnemy.name = arraysOfEnemy[randomEnemyIndex].name;
            goEnemy.transform.SetParent(enemiesHolder);

            amountOfEnemiesOnLevel++;
        }
    }

    private void SpawnRoundedEdges(int x, int y)
    {
        if (useRoundedEdges)
        {
            Collider2D hitWall = Physics2D.OverlapBox(new Vector2(x, y), Vector2.one * 0.8f, 0, wallMask);

            if (hitWall)
            {
                Collider2D hitTop = Physics2D.OverlapBox(new Vector2(x, y + 1), Vector2.one * 0.8f, 0, wallMask);
                Collider2D hitRight = Physics2D.OverlapBox(new Vector2(x + 1, y), Vector2.one * 0.8f, 0, wallMask);
                Collider2D hitLeft = Physics2D.OverlapBox(new Vector2(x - 1, y), Vector2.one * 0.8f, 0, wallMask);
                Collider2D hitBottom = Physics2D.OverlapBox(new Vector2(x, y - 1), Vector2.one * 0.8f, 0, wallMask);

                int bitValue = 0;

                if (!hitTop) { bitValue += 1; }
                if (!hitRight) { bitValue += 2; }
                if (!hitBottom) { bitValue += 4; }
                if (!hitLeft) { bitValue += 8; }

                if (bitValue > 0)
                {
                    GameObject goEdge = Instantiate(roundedEdges[bitValue], new Vector2(x, y), Quaternion.identity);
                    goEdge.name = roundedEdges[bitValue].name;
                    goEdge.transform.SetParent(hitWall.transform);
                }

            }
        }

    }


    /// <summary>
    /// choose a random position for potential Enemy
    /// </summary>
    /// <returns></returns>
    private Vector3 randomPosition()
    {
        // 15 means two units on player's pos at the start, and count -3 means two units at the exit
        int randomIndex = Random.Range(15, floorPosition.Count - 3);

        Vector3 randomPosition = floorPosition[randomIndex];
        floorPosition.RemoveAt(randomIndex);

        return randomPosition;
    }


    private IEnumerator DelayProgress(bool isBoosRoom = false)
    {
        for (int i = 0; i < floorList.Count; i++)
        {
            GameObject goTile = Instantiate(tilePrefab, floorList[i], Quaternion.identity);
            floorPosition.Add(goTile.transform.position); // initializing a possible position for enemies
            goTile.name = tilePrefab.name;
            goTile.transform.SetParent(this.transform);
        }

        while (FindObjectsOfType<TileSpawner>().Length > 0)
        {
            yield return null;
        }

        if (!isBoosRoom)
        {
            SpawnExit();
            SpawnObjects(interactiveItems, interactiveItemsCount.minimum, interactiveItemsCount.maximum);
            SpawnObjects(staticItems, staticItemsCount.minimum, staticItemsCount.maximum);
            SpawnEnemies(randomEnemies, enemiesCount.minimum, enemiesCount.maximum);
        }

        //Spawning rounded Edges
        for (int x = (int)minX - 2; x <= (int)maxX + 2; x++)
        {
            for (int y = (int)minY - 2; y <= (int)maxY; y++)
            {
                SpawnRoundedEdges(x, y);
            }
        }

        Debug.Log("Generation complete!"); // Call a A* scan after generation terrain
        aStarManager.EnableScan();

        levelManager.FindNewDungeonManagerObject(); // Make a connection between this and level manager Game object
        levelManager.UpdateEnemyCountOnNewLevel(amountOfEnemiesOnLevel);

    }

}
