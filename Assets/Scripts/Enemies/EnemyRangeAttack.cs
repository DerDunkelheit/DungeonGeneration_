using System.Collections;
using Attacking;
using UnityEngine;

namespace Enemies
{
    public class EnemyRangeAttack : Attack
    {
        [Header("General Fields")]
        [SerializeField] GameObject enemyProjectilePrefab = null;
        [SerializeField] float fireRate = 0.5f;

        protected override void Start()
        {
            base.Start();

            StartCoroutine(AttackRoutine());
        }

        public override void DoAttack()// Interface's method
        {
            GameObject proj = Instantiate(enemyProjectilePrefab, this.transform.position, Quaternion.identity);
            proj.GetComponent<EnemyProjectile>().SetDirection(CalculateDirToPlayer(),proj.transform.rotation);
        }

        private Vector2 CalculateDirToPlayer()
        {
            Vector2 dir = playerHealth.transform.position - this.transform.position;
            dir.Normalize();

            return dir;
        }

        private Quaternion CalculateRotationToPlayer()
        {
            Quaternion rot = new Quaternion();

            Vector2 targetDir = playerHealth.transform.position - this.transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * (180 / Mathf.PI);

            rot = Quaternion.AngleAxis(angle, Vector3.forward);
      

            return rot;
        }


        private IEnumerator AttackRoutine()
        {
            while (true)
            {
                yield return null;

                if (enemyPathfinding.isPlayerInAttackRange)
                {
                    DoAttack();
                    yield return new WaitForSeconds(fireRate);
                }
            }

        }
    }
}
