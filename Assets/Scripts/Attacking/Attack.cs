using Enemies;
using Player_Abilities_Stats.Health;
using UnityEngine;

namespace Attacking
{
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
}

