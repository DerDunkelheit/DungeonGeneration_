using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    [SerializeField] GameObject floorPrefab = null;
    [SerializeField] GameObject wallPrefab = null;
    [SerializeField] int roomWidth = 8;
    [SerializeField] int roomHeight = 8;

    void Start()
    {
        GenerateRoom(roomWidth, roomHeight);
    }

    private void GenerateRoom(int width, int height)
    {
        for (int x = -1; x <= width; x++)
        {
            for (int y = -1; y <= height; y++)
            {
                if (x == width || y == height || x == -1 || y == -1)
                {
                    GameObject wall = Instantiate(wallPrefab, new Vector2(x, y), Quaternion.identity);
                    wall.gameObject.name = wallPrefab.name;
                    wall.transform.SetParent(this.transform);

                    continue;
                }

                GameObject floor = Instantiate(floorPrefab, new Vector2(x, y), Quaternion.identity);
                floor.gameObject.name = floorPrefab.name;
                floor.transform.SetParent(this.transform);
            }
        }
    }



}
