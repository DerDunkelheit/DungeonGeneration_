using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab = null;
    
    void Awake()
    {
      Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

  
}
