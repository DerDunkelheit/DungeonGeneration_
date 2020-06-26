using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public abstract class InteractiveObjectBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected BoxCollider2D coll2D;

    protected bool isPlayerInRange;

    protected virtual void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        coll2D = this.GetComponent<BoxCollider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    protected virtual  void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
