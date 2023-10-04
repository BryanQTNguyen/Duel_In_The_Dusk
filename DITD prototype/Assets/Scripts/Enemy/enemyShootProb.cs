using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        enemyShootReset();
    }

    // Update is called once per frame
    void Update()
    {
        if(skillCheck.enemyTurnToShoot == true && fireIndex == 0)
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
                fireIndex = 0;
                Debug.Log("Gun Jammed");
                skillCheck.secondChance();


            }
        }

        if (CheckScript.playerShotAcc == true)
        {
            skillCheck.enemyTurnToShoot = false;
            anim.SetBool("isDeadEnemy", true);
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
            skillCheck.secondChance();
            skillCheck.PlayerDamage();


        }
        else
        {
            Debug.Log("Miss!");
            skillCheck.enemyTurnToShoot = false;
            skillCheck.secondChance();
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
