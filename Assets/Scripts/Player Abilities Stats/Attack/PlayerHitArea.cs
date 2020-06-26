using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PlayerHitArea : MonoBehaviour, IAttack
{

    [Header("Effects Fields")]
    [SerializeField] GameObject attackEffectGo = null;

    PlayerAttackAbility attackAbility;

    AIPath enemyAiPath;

    void Start()
    {
        attackAbility = this.GetComponentInParent<PlayerAttackAbility>();
    }

    public void DoAttack() // Interface's method
    {

    }



    private void KnockBack(Transform targetTrans)
    {
        Vector2 difference = targetTrans.position - this.transform.position;
        targetTrans.position = new Vector2(targetTrans.position.x + difference.x, targetTrans.position.y + difference.y);
    }


    void OnEnable() // method sets GO. with animation attack effect tu true, to play animation
    {
        attackEffectGo.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage((int)attackAbility.Damage);

            KnockBack(health.transform);
        }

    }
}
