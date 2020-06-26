using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupObjectBase : MonoBehaviour
{


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
        }
    }
}
