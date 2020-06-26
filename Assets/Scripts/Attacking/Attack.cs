using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour,IAttack
{
    public enum AttackType
    {
        Meele,
        Range
    };

    [Header("Genetal Fields")]
    public AttackType attackType = AttackType.Meele;
    public float dmgAmount = 1;

    protected Health playerHealth;
    protected EnemyPathfinding enemyPathfinding;

    protected virtual void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        enemyPathfinding = this.GetComponent<EnemyPathfinding>();
    }

    public abstract void DoAttack();
}

