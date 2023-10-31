using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour
{
    public float nextWayPointDistance = 3f;
    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    public float speed;
    public float maxSpeed = 2f;
    public float checkRadius;
    public float attackRadius;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;

    public DialgoueManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        target = GameObject.FindWithTag("Player").transform;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private void Update()
    {
        if(manager.isActive == false)
        {
            anim.SetBool("isRunning", isInChaseRange);
            isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
            isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

            dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            dir.Normalize();
            movement = dir;
        }

 
        anim.SetFloat("x", dir.x);
        anim.SetFloat("y", dir.y);

        /* this is for creating idles for the different axes when thats given
        if (Input.GetAxisRaw("x") == 1 || Input.GetAxisRaw("x") == -1 || Input.GetAxisRaw("y") == 1 || Input.GetAxisRaw("y") == -1)
        {
            anim.SetFloat("IdleX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("IdleY", Input.GetAxisRaw("Vertical"));
        }
        */
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
        Vector2 force = direction * speed;

        rb.velocity=force;
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance && isInChaseRange)
        {
            speed = maxSpeed;
            currentWayPoint++;

        }else if(isInChaseRange == false)
        {
            speed = 0;
            anim.SetBool("isRunning", false);
        }
        if (manager.isActive == true)
        {
            speed = 0;
            anim.SetBool("isRunning", false);

        }
        /*
        if (isInAttackRange)
        {
            Debug.Log("Battle Starts");
        }
        if (isInChaseRange && !isInAttackRange)
        {
            MoveCharacter(movement);
        }
        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
        */
    }
    private void MoveCharacter(Vector2 dir)
    {

        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }
}
