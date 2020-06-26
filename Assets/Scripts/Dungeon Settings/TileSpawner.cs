using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    DungeonManager dungManager;

    void Awake()
    {
        dungManager = FindObjectOfType<DungeonManager>();

        InstantiateFloorOnPos(this.transform.position);

        if(transform.position.x > dungManager.maxX)
        {
            dungManager.maxX = this.transform.position.x;
        }
        if(transform.position.x < dungManager.minX)
        {
            dungManager.minX = this.transform.position.x;
        }
        if(transform.position.y > dungManager.maxY)
        {
            dungManager.maxY = this.transform.position.y;
        }
        if(transform.position.y < dungManager.minY)
        {
            dungManager.minY = this.transform.position.y;
        }
    }

    void Start()
    {
        LayerMask envMask = LayerMask.GetMask("Wall", "Floor");
        Vector2 hitSize = Vector2.one * 0.8f;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 targetPos = new Vector2(this.transform.position.x + x, this.transform.position.y + y);
                Collider2D hit = Physics2D.OverlapBox(targetPos, hitSize, 0, envMask);

                if (!hit)
                {
                    InstantiateWallOnPos(targetPos);
                }
            }
        }

        Destroy(this.gameObject);
    }

    private void InstantiateFloorOnPos(Vector3 pos)
    {
        GameObject goFloor = Instantiate(dungManager.floorPrefab, pos, Quaternion.identity);
        goFloor.name = dungManager.floorPrefab.name;
        goFloor.transform.SetParent(dungManager.transform);
    }

    private void InstantiateWallOnPos(Vector3 pos)
    {
        GameObject goWall = Instantiate(dungManager.wallPrefab, pos, Quaternion.identity);
        goWall.name = dungManager.wallPrefab.name;
        goWall.transform.SetParent(dungManager.transform);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(this.transform.position, Vector3.one);
    }
}
