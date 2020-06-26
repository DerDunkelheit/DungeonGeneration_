using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    [SerializeField] LayerMask obstacleMask = 0;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(CheckLayer(other.gameObject.layer,obstacleMask))
        {
            Debug.Log("Player Hit!");
            Destroy(this.gameObject);
        }
    }
}
