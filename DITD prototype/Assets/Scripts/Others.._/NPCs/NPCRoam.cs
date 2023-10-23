using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class NPCRoam : MonoBehaviour
{
    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    public float speed;
    public float stopToTalkRadius;

    private Transform target;
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
        anim = GetComponent<Animator>();
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
        if(manager.isActive == false)
        {
            anim.SetBool("isWalking", true);
        }
    }
}
