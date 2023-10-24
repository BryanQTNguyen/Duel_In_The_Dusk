using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class NPCRoam : MonoBehaviour
{
    public float nextWayPointDistance = 3f;
    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    public float speed;
    public float stopToTalkRadius;

    public LayerMask whatIsPoint;


    public Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 dir;

    private bool isInStopToTalkRange;
    public DialgoueManager manager;


    // Start is called before the first frame update
    void Start()
    {
        //initialize the needed variables
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();


        //making them always have a path
        InvokeRepeating("UpdatePath", 0f, .5f);

    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (manager.isActive == false)
        {
            isInStopToTalkRange = Physics2D.OverlapCircle(transform.position, stopToTalkRadius, whatIsPoint);

            dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            dir.Normalize();
            movement = dir;
        }
    }

    private void FixedUpdate()
    {
        if (path == null)
            return;
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
        else if (isInStopToTalkRange == true)
        {
            rb.velocity = Vector2.zero;
        }
        if (manager.isActive == true)
        {
            rb.velocity = Vector2.zero;

        }
    }
}