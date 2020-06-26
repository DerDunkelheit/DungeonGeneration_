using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class AStarManager : MonoBehaviour
{

    

    void Start()
    {
      //  StartCoroutine(WaitBeforeScan());
    }

    public void EnableScan()
    {
        AstarPath.active.Scan();
    }

    

    private IEnumerator WaitBeforeScan()
    {
        yield return new WaitForSeconds(1f);
        AstarPath.active.Scan();
    }
}
