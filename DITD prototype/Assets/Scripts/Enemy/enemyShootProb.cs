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
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;

    public int lives;

    public int fireIndex;

    public int probOfLanding; //will their shot land? This number will be randomized between 0-100
    public int probOfLandingTarget;
    public int bleedRate;
    public int damage;
    public int headShotRate;

    public bool kill; //the player is dead que

    public bool secondChanceTime;
    public float secondChanceTimer;

    //Different types of characters the rates for them
    private int sheriffProb = 80; //landing another shot
    private int sheriffHead = 20; //headshot rate
    private int sheriffDamageAmount = 20;
    private int sheriffLives = 5;
    private int sheriffBleedRate = 50; 

    private int bankerProb = 50;
    private int bankerHead = 0;
    private int bankerDamageAmount = 50;
    private int bankerLives = 3;
    private int bankerBleedRate = 5;

    private int deputyProb = 70;
    private int deptyHead = 5;
    private int deptyDamageAmount = 10;
    private int deptyLives = 1;
    private int deptyBleedRate = 10;

    private int rangerProb = 70;
    private int rangerHead = 5;
    private int rangerDamageAmount = 10;
    private int rangerLives = 1;
    private int rangerBleedRate = 10;

    private int cactusProb = 70;
    private int cactusHead = 10;
    private int cactusDamageAmount = 1;
    private int cactusLives = 5;
    private int cactusBleedRate = 10;

    // Start is called before the first frame update
    void Start()
    {
        GameManagerObj = GameObject.Find("gameManager");
        GameManager = GameManagerObj.GetComponent<gameManager>();
        if(GameManager.enemyType != 0)
        {
            if(GameManager.enemyType == 1)
            {
                probOfLandingTarget= deputyProb;
                bleedRate = deptyBleedRate;
                damage = deptyDamageAmount;
                headShotRate = deptyHead;

            }
            if (GameManager.enemyType == 2)
            {
                probOfLandingTarget = deputyProb;
                bleedRate = deptyBleedRate;
                damage = deptyDamageAmount;
                headShotRate = deptyHead;

            }
            if (GameManager.enemyType == 3)
            {
                probOfLandingTarget = deputyProb;
                bleedRate = deptyBleedRate;
                damage = deptyDamageAmount;
                headShotRate = deptyHead;

            }
            if (GameManager.enemyType == 4)
            {
                probOfLandingTarget = deputyProb;
                bleedRate = deptyBleedRate;
                damage = deptyDamageAmount;
                headShotRate = deptyHead;

            }
            if (GameManager.enemyType == 5)
            {
                probOfLandingTarget = deputyProb;
                bleedRate = deptyBleedRate;
                damage = deptyDamageAmount;
                headShotRate = deptyHead;

            }
        }
        else
        {
            Debug.Log("Something sus is going on here");
        }
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

            GameManager.Damage(damage*2);

            skillCheck.enemyTurnToShoot = false;

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
