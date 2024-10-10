using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAi : MonoBehaviour
{
    public Transform target;
    public Transform enemyGfx;
    [SerializeField] private Animator animator;

    public float speed;
    public float nextWayPointDistance =3f;
    private float distanceTargetEnemy;
    public float distanceMin;

    Path path;
    int currentWayPoint = 0;
    bool reachedEndPoint = false;

    Seeker seeker;
    

    void Start()
    {        
        seeker = GetComponent<Seeker>();    

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (target != null)
        {
            distanceTargetEnemy = Vector2.Distance(transform.position, target.position);
            if (seeker.IsDone() && distanceTargetEnemy < distanceMin)
                seeker.StartPath(transform.position, target.position, OnPathComplet);
        }
    }    

    private void OnPathComplet(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
            return;
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndPoint = true;
            return;
        }
        else
            reachedEndPoint = false;

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - new Vector2 (transform.position.x,transform.position.y)).normalized;

        if (distanceTargetEnemy > 4f)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
        
        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }
}
