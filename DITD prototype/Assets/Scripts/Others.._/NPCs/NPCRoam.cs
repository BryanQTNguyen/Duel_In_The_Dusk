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

    public Transform target;
    public GameObject Target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 dir;

    private bool isInStopToTalkRange;
    public DialgoueManager manager;
    private bool isWalking;
    public bool uwu = false;


    // Start is called before the first frame update
    void Start()
    {
        //initialize the needed variables
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        anim = GetComponent<Animator>();


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
        if(uwu == false)
        {
            if (manager.isActive == false)
            {
                speed = 1f;
                dir = target.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                dir.Normalize();
                movement = dir;
            }
            else
            {
                speed = 0f;
            }

        }
        anim.SetBool("isWalking", isWalking);
        if (speed == 0f)
        {
            isWalking = false;
        }
        else if (speed == 1f)
        {
            isWalking = true;
        }
        anim.SetFloat("x", dir.x);
        anim.SetFloat("y", dir.y);
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

        rb.velocity = force;
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            uwu = true;
            isWalking = false;
            speed = 0;
        }
        else
        {
            Target.GetComponent<wayPointScript>().RandomPos();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            uwu = true;
            isWalking = false;
            speed = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            uwu = false;
            isWalking = true;
            speed = 1;
        }
    }
}