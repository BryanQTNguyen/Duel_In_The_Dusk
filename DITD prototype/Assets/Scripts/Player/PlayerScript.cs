using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //this scirpt will mainly communicate with the game manager
    public float damageType;
    /*
    1 = regular shot (33 damage)
    2 = instantKill (just reduce the health to 0 right away)
    3 = mini shot (reduce the health by 10)
    */
    public bool bleed;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
