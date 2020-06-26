using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [Header("Generals Settings")]
        [SerializeField] float minWaitTime = 1f;
        [SerializeField] float maxWaitTIme = 5f;

        [Header("Player Detect Fields")]
        [SerializeField] float alertRange = 1f;
        [SerializeField] float chaseSpeed = 2;
        [Tooltip("In percents")] [Range(0, 100)] [SerializeField] float missChance = 50;
        [SerializeField] float dmgAmount = 1;


        Transform player;
        LayerMask obstackleMask;
        LayerMask walkableMask;
        Vector2 curPos;
        List<Vector2> avaliableMovementList = new List<Vector2>();
        List<Node> nodesList = new List<Node>();
        bool isMoving;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            obstackleMask = LayerMask.GetMask("Wall", "Enemy", "Player");
            walkableMask = LayerMask.GetMask("Wall", "Enemy");
            curPos = this.transform.position;

            StartCoroutine(Movement());
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

        private void Attack()
        {
            int roll = Random.Range(0, 100);
            if (roll > missChance)
            {
                Debug.Log($"{name}:attacked and hit for {dmgAmount} points of damage");
            }
            else
            {
                Debug.Log($"{name}:attaked and missed");
            }
        }

        private void CheckNode(Vector2 chkPoint, Vector2 parent)
        {
            Vector2 size = Vector2.one * 0.5f;
            Collider2D hit = Physics2D.OverlapBox(chkPoint, size, 0, walkableMask);

            if (!hit)
            {
                nodesList.Add(new Node(chkPoint, parent));
            }
        }

        private Vector2 FindNextStep(Vector2 startPos, Vector2 targetPos)
        {
            int listIdex = 0;
            Vector2 myPosition = startPos;
            nodesList.Clear();
            nodesList.Add(new Node(startPos, startPos));

            while (myPosition != targetPos && listIdex < 1000 && nodesList.Count > 0)
            {
                CheckNode(myPosition + Vector2.up, myPosition); 
                CheckNode(myPosition + Vector2.right, myPosition);
                CheckNode(myPosition + Vector2.down, myPosition);
                CheckNode(myPosition + Vector2.left, myPosition);
                listIdex++;
                if (listIdex < nodesList.Count)
                {
                    myPosition = nodesList[listIdex].position;
                }
            }

            if (myPosition == targetPos)
            {
                nodesList.Reverse();
                for (int i = 0; i < nodesList.Count; i++)
                {
                    if(myPosition == nodesList[i].position)
                    {
                        if(nodesList[i].parent == startPos)
                        {
                            return myPosition;
                        }

                        myPosition = nodesList[i].parent;
                    }
                }
            }

            return startPos;
        }

        private IEnumerator SmoothMovement(float speed)
        {
            isMoving = true;

            while (Vector2.Distance(this.transform.position, curPos) > 0.01f)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, curPos, 5f * Time.deltaTime);
                yield return null;
            }


            this.transform.position = curPos;
            yield return new WaitForSeconds(speed);

            isMoving = false;
        }

        private IEnumerator Movement()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (!isMoving)
                {
                    float dist = Vector2.Distance(this.transform.position, player.position);

                    if (dist <= alertRange)
                    {
                        Debug.Log("Player Entered in alerRange");

                        if (dist <= 1.1f)
                        {
                            Attack();
                            yield return new WaitForSeconds(Random.Range(0.5f, 1.15f));
                        }
                        else
                        {
                            Vector2 newPos = FindNextStep(this.transform.position, player.transform.position);
                            if (newPos != curPos)
                            {
                                curPos = newPos;
                                StartCoroutine(SmoothMovement(chaseSpeed));
                            }
                            else
                            {
                                Patrol();
                            }
                        }
                    }
                    else
                    {
                        Patrol();
                    }
                }

  
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(this.transform.position,alertRange);
        }
    }
}
