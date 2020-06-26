using Pathfinding;
using UnityEngine;

namespace Enemies
{
    public class CustomEnemyWithPathTest : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float speed = 2f;
        [SerializeField] float nextWaypointDistance = 3f;

        Path path;
        Seeker seeker;
        Rigidbody2D rb;
        int currentWaypoint = 0;
        //bool reachedEndOfPath = false;

        void Start()
        {
            seeker = this.GetComponent<Seeker>();
            rb = this.GetComponent<Rigidbody2D>();

            InvokeRepeating("UpdatePath", 0, 0.5f);

        }

        void FixedUpdate()
        {
            if(path == null) { return; }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                //reachedEndOfPath = true;
                return;
            }
            else
            {
                //reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.fixedDeltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if(distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }


        private void UpdatePath()
        {
            if(seeker.IsDone())
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        }

        private void OnPathComplete( Path p)
        {
            if(!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }


    }
}
