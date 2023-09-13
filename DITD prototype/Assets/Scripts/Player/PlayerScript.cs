using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Health variables
    public int HealthCurrent;
    public int HealthMax = 100;
    public PlayerHealth healthBar;

    public Rigidbody2D rb;

    //Weapon variables 
    public int Ammo;
    public int gunType; // public variable made to inform other scripts that the player has this gun equipped


    // Start is called before the first frame update
    void Start()
    {
        gunType = 0;
        Ammo = 6; //character starts with revolver
        HealthCurrent = HealthMax;
        healthBar.SetMaxHealth(HealthMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthCurrent <= 0)
        {
            Death();
        }
        /* health bar test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(10);
        }
        */
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shotgun")
        {
            Ammo = 2;
            gunType = 1; 
        }
        if (collision.gameObject.tag == "Rifle")
        {
            Ammo = 12;
            gunType = 2;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
            rb.velocity = Vector2.zero;
    }
    public void Damage(int amount) //damage function with a parameter for varying damages
    {
        HealthCurrent = HealthCurrent - amount;
        healthBar.SetHealth(HealthCurrent);
    }
    public void Death()
    {
        //load ending screen
    }
}
