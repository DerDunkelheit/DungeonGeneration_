using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Enemies
{
    public class EnemyPathfinding : MonoBehaviour
    {
        [Header("Generals Settings")]
        [SerializeField] float minWaitTime = 1f;
        [SerializeField] float maxWaitTIme = 5f;

        [Header("Player Detect Fields")]
        [SerializeField] float alertRange = 2f;
        [SerializeField] float attackRange = 1.5f;
        [SerializeField] float followPlayerSpeed = 1f;

        public bool isPlayerInAttackRange = false;

        Transform player;
        LayerMask obstackleMask;
        LayerMask walkableMask;
        Vector2 curPos;
        List<Vector2> avaliableMovementList = new List<Vector2>();
        List<Vector2> pathingMovementList = new List<Vector2>();
        List<Node> nodesList = new List<Node>();
        bool isMovingRoutine = false;
        bool canPathfinding = false;

        AIPath aIPath;
        EnemyAttack enemyAttack;
        AIDestinationSetter destinationSetter;

        Vector2 newPos;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            obstackleMask = LayerMask.GetMask("Wall", "Enemy", "Player");
            walkableMask = LayerMask.GetMask("Wall", "Enemy");
            curPos = this.transform.position;

            aIPath = this.GetComponent<AIPath>();
            aIPath.maxSpeed = followPlayerSpeed;
            aIPath.endReachedDistance = attackRange;

            destinationSetter = this.GetComponent<AIDestinationSetter>();
            destinationSetter.target = player;

            enemyAttack = this.GetComponent<EnemyAttack>();

            StartCoroutine(Movement());
        }

        void Update()
        {
            float pathDist =Vector2.Distance(this.transform.position, player.position);
            if(pathDist <= alertRange)
            {
                canPathfinding = true;
            }
            else
            {
                canPathfinding = false;
            }
        }

        private void Patrol()
        {
            avaliableMovementList.Clear();

            Vector2 size = Vector2.one * 0.8f;
            Collider2D hitUp = Physics2D.OverlapBox(curPos + Vector2.up, size, 0f, obstackleMask);
            if (!hitUp) { avaliableMovementList.Add(Vector2.up); }

            Collider2D hitLeft = Physics2D.OverlapBox(curPos + Vector2.left, size, 0f, obstackleMask);
            if (!hitLeft) { avaliableMovementList.Add(Vector2.left); }

            Collider2D hitBottom = Physics2D.OverlapBox(curPos + Vector2.down, size, 0f, obstackleMask);
            if (!hitBottom) { avaliableMovementList.Add(Vector2.down); }

            Collider2D hitRight = Physics2D.OverlapBox(curPos + Vector2.right, size, 0f, obstackleMask);
            if (!hitRight) { avaliableMovementList.Add(Vector2.right); }

            if (avaliableMovementList.Count > 0)
            {
                int randomIndex = Random.Range(0, avaliableMovementList.Count);
                curPos += avaliableMovementList[randomIndex];
            }

            StartCoroutine(SmoothMovement(Random.Range(minWaitTime, maxWaitTIme)));
        }


        #region myTestPathFInding
        //private Vector2 myFindNextStep(Vector2 startPos, Vector2 targetPos)
        //{
        //    pathingMovementList.Clear();
        //    Vector2 myPos = startPos;
        //    float dist = Vector2.Distance(myPos, targetPos);

        //    Vector2 size = Vector2.one * 0.8f;
        //    Collider2D hitUp = Physics2D.OverlapBox(curPos + Vector2.up, size, 0f, obstackleMask);
        //    if (!hitUp)
        //    {
        //        if (Vector2.Distance(myPos + Vector2.up, targetPos) < dist)
        //        {
        //            pathingMovementList.Add(myPos + Vector2.up);
        //        }
        //    }

        //    Collider2D hitLeft = Physics2D.OverlapBox(curPos + Vector2.left, size, 0f, obstackleMask);
        //    if (!hitLeft)
        //    {
        //        if (Vector2.Distance(myPos + Vector2.left, targetPos) < dist)
        //        {
        //            pathingMovementList.Add(myPos + Vector2.left);
        //        }
        //    }

        //    Collider2D hitBottom = Physics2D.OverlapBox(curPos + Vector2.down, size, 0f, obstackleMask);
        //    if (!hitBottom)
        //    {
        //        if (Vector2.Distance(myPos + Vector2.down, targetPos) < dist)
        //        {
        //            pathingMovementList.Add(myPos + Vector2.down);
        //        }
        //    }

        //    Collider2D hitRight = Physics2D.OverlapBox(curPos + Vector2.right, size, 0f, obstackleMask);
        //    if (!hitRight)
        //    {
        //        if (Vector2.Distance(myPos + Vector2.right, targetPos) < dist)
        //        {
        //            pathingMovementList.Add(myPos + Vector2.right);
        //        }
        //    }

        //    if (pathingMovementList.Count > 0)
        //    {
        //        int randomIndex = Random.Range(0, pathingMovementList.Count);

        //        return pathingMovementList[randomIndex];
        //    }

        //    return myPos;

        //}

        #endregion

        private IEnumerator SmoothMovement(float speed)
        {
            isMovingRoutine = true;

            while (Vector2.Distance(this.transform.position, curPos) > 0.01f)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, curPos, 5f * Time.deltaTime);
                yield return null;
            }


            this.transform.position = curPos;
            yield return new WaitForSeconds(speed);

            isMovingRoutine = false;
        }

        private IEnumerator Movement()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);

                if (!isMovingRoutine || canPathfinding)
                {
                    float dist = Vector2.Distance(this.transform.position, player.position);
                    if (dist <= alertRange)
                    {
                        if (dist <= attackRange)
                        {
                            isPlayerInAttackRange = true;
                        }
                        else
                        {
                            isPlayerInAttackRange = false;
                            aIPath.enabled = true;
                            curPos = this.transform.position;
                        }
                    }
                    else
                    {
                        Patrol();
                        aIPath.enabled = false;
                    }

                }
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(this.transform.position, alertRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, attackRange);
        }
    }
}
