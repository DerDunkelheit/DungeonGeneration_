using System.Collections;
using Attacking;
using UnityEngine;

namespace Enemies
{
    public class EnemyAttack : Attack
    {
        [Tooltip("In percents")] [Range(0, 100)] public float missChance = 50;
        public float minAttackDelay = 0.5f;
        public float maxAttackDelay = 1.5f;


        protected override void Start()
        {
            base.Start();
            StartCoroutine(AttackRoutine());
        }

        private void TryAttack()
        {
            int roll = Random.Range(0, 100);
            if (roll > missChance)
            {
                Debug.Log($"{name}:attacked and hit for {dmgAmount} points of damage");
                DoAttack();
            }
            else
            {
                Debug.Log($"{name}:attaked and missed");
            }
        }

        public override void DoAttack()
        {
            playerHealth.TakeDamage((int)dmgAmount);
        }

        private IEnumerator AttackRoutine()
        {
            while (true)
            {
                yield return null;

                if (enemyPathfinding.isPlayerInAttackRange)
                {
                    TryAttack();
                    yield return new WaitForSeconds(Random.Range(minAttackDelay, maxAttackDelay));
                }
            }

        }
    }
}
