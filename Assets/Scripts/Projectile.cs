using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5;
    [SerializeField] float acceleration = 0f;
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }

    protected Rigidbody2D rb;

    Vector2 movement;

    protected virtual void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Speed = projectileSpeed;
    }

    protected virtual void Update()
    {
        MoveProjectile();
    }

    protected void MoveProjectile()
    {
        movement = Direction * projectileSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        Speed += acceleration * Time.deltaTime;
    }

    public void  SetDirection(Vector2 newDirection, Quaternion rotation)
    {
        Direction = newDirection;
        transform.rotation = rotation;
    }


    protected  bool CheckLayer(int layer, LayerMask objectMask)
    {
        return ((1 << layer) & objectMask) != 0;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
