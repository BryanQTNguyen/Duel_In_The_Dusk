using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using UnityEngine;

public class enemyShootProb : MonoBehaviour
{
    // references to other objects in the game
    [SerializeField] SkillCheck skillCheck;
    [SerializeField] checkScript CheckScript;
    [SerializeField] Animator anim;
    [SerializeField] shake Shake;

    public int lives;

    public int fireIndex;

    public int probOfLanding; //will their shot land? This number will be randomized between 0-100
    public int probOfLandingTarget;
    public int bleedRate; 

    public int headShotRate;

    public bool kill; //the player is dead que

    public bool secondChanceTime;
    public float secondChanceTimer;

    //Different types of characters the rates for them
    private int sheriffProb = 80; //landing the initial shot
    private int sheriffHead = 10; //headshot rate



    // Start is called before the first frame update
    void Start()
    {
        enemyShootReset();
        secondChanceTime = false;
        secondChanceTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (skillCheck.enemyTurnToShoot == true && fireIndex == 0)
        {
            if (fireIndex == 0)
            {
                fireIndex++;
                anim.SetTrigger("isShootEnemy");
                enemyFire();
            }
        }

        if (CheckScript.playerShotAcc == true)
        {
            skillCheck.enemyTurnToShoot = false;
            anim.SetBool("isDeadEnemy", true);
        }
    }

    void FixedUpdate()
    {
        if (secondChanceTime)
        {
            secondChanceTimer = secondChanceTimer + Time.deltaTime;

            if(secondChanceTimer >= 1.3f)
            {
                skillCheck.secondChance();
                enemyShootReset();
                secondChanceTime = false;
                secondChanceTimer = 0f;
            }
        }
    }


    private void enemyFire()
    {
        probOfLanding = Random.Range(0, 100);
        if (probOfLanding <= headShotRate)
        {
            Debug.Log("Player got head shotted");
            Shake.enemyShotShake();
            kill = true;
            skillCheck.PlayerDamage();
            skillCheck.enemyTurnToShoot = true;

        }
        else if(probOfLanding > headShotRate && probOfLanding <= probOfLandingTarget)
        {
            Debug.Log("Player got hit with a crippling shot");
            int bleedProbability = Random.Range(0, 100);
            
            if(bleedProbability <= bleedRate) // bleed controller
            {
                //set bleed to be true
            }
            else
            {
                bleedProbability = 0;
            }

            Shake.enemyShotShake(); //shakes the screen

            kill = false;
            secondChanceTime = true;
            skillCheck.PlayerDamage();
            skillCheck.enemyTurnToShoot = false;

        }
        else
        {
            Debug.Log("Miss!");
            skillCheck.enemyTurnToShoot = false;
            secondChanceTime = true;
            enemyShootReset();


        }
    }

    private void enemyShootReset()
    {
        fireIndex = 0;
        probOfLanding = 0;
    }
}
