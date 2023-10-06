using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class enemyShootProb : MonoBehaviour
{
    // references to other objects in the game
    [SerializeField] SkillCheck skillCheck;
    [SerializeField] checkScript CheckScript;
    [SerializeField] int fireIndex;
    [SerializeField] Animator anim;
    [SerializeField] shake Shake;



    public int probOfShooting; //will they shoot the gun?
    public int probOfLanding; //will their shot land?
    public bool kill; //the player is dead que
    public bool secondChanceTime;
    public float secondChanceTimer;

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
            probOfShooting = Random.Range(0, 100);
            if (probOfShooting < 70)
            {
                anim.SetTrigger("isShootEnemy");
                enemyFire();
                fireIndex++;
            }
            else if (probOfShooting > 70)
            {
                skillCheck.enemyTurnToShoot = false;
                fireIndex++;
                Debug.Log("Gun Jammed");
                secondChanceTime = true;
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

            if(secondChanceTimer >= 2f)
            {
                skillCheck.secondChance();
                secondChanceTime = false;
                secondChanceTimer = 0f;
            }
        }
    }


    private void enemyFire()
    {
        probOfLanding = Random.Range(0, 100);
        if (probOfLanding <= 10)
        {
            Debug.Log("Player got head shotted");
            Shake.enemyShotShake();
            kill = true;
            skillCheck.PlayerDamage();
        }

        else if(probOfLanding > 10 && probOfLanding <= 70)
        {
            Debug.Log("Player got hit with a crippling shot");
            Shake.enemyShotShake();
            kill = false;
            secondChanceTime = true;
            skillCheck.PlayerDamage();
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
        probOfShooting = 0;
        probOfLanding = 0;
    }
}
