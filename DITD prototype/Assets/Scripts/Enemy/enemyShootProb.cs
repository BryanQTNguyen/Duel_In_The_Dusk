using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using UnityEngine;

public class enemyShootProb : MonoBehaviour
{
    // references to other objects in the game
    [SerializeField] SceneController controller;
    [SerializeField] SkillCheck skillCheck; 
    [SerializeField] checkScript CheckScript;
    [SerializeField] Animator anim;
    [SerializeField] Animator playerAnim;
    [SerializeField] shake Shake;
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;

    public int lives;

    public int fireIndex;
    private int livesIndex = 0;

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
    private int sheriffLives = 4;
    private int sheriffBleedRate = 50; 

    private int bankerProb = 50;
    private int bankerHead = 0;
    private int bankerDamageAmount = 50;
    private int bankerLives = 2;
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
        enemyStatSet();


        enemyShootReset();
        secondChanceTime = false;
        secondChanceTimer = 0f;
    }

    public void enemyStatSet()
    {
        if (GameManager.enemyType != 0) // this checks the enemy type and sets the proper variables accordingly 
        {
            if (GameManager.enemyType == 1)
            {
                probOfLandingTarget = deputyProb;
                bleedRate = deptyBleedRate;
                damage = deptyDamageAmount;
                headShotRate = deptyHead;
                lives = deptyLives;


            }
            if (GameManager.enemyType == 2)
            {
                probOfLandingTarget = rangerProb;
                bleedRate = rangerBleedRate;
                damage = rangerDamageAmount;
                headShotRate = rangerHead;
                lives = rangerLives;



            }
            if (GameManager.enemyType == 3)
            {
                probOfLandingTarget = cactusProb;
                bleedRate = cactusBleedRate;
                damage = cactusDamageAmount;
                headShotRate = cactusHead;
                lives = cactusLives;



            }
            if (GameManager.enemyType == 4)
            {
                probOfLandingTarget = bankerProb;
                bleedRate = bankerBleedRate;
                damage = bankerDamageAmount;
                headShotRate = bankerHead;
                lives = bankerLives;

            }
            if (GameManager.enemyType == 5)
            {
                probOfLandingTarget = sheriffProb;
                bleedRate = sheriffBleedRate;
                damage = sheriffDamageAmount;
                headShotRate = sheriffHead;
                lives = sheriffLives;
            }
        }
        else //this shows that no enemy type is being recorded meaning something sus is happening here
        {
            Debug.Log("Something sus is going on here");
        }
    }

    // Update is called once per frame
    void Update()
    {
 
        if (skillCheck.enemyTurnToShoot == true && fireIndex == 0)
        {
            if (fireIndex == 0)
            {
                fireIndex++;
                StartCoroutine(enemyShootAndAnimation());
            }
        }

        if (CheckScript.playerShotAcc == true) // check if player shot has landed
        {
            if(livesIndex == 0)
            {
                StartCoroutine(playerAccurate());
                livesIndex = 1;
            }


        }
    }
    private IEnumerator playerAccurate()
    {
        yield return new WaitForSeconds(1f);
        if (lives > 0)
        {
            lives = lives - 1;
            secondChanceTime = true;
            skillCheck.enemyTurnToShoot = false;
            anim.SetTrigger("isDamageEnemy");
            CheckScript.playerShotAcc = false;
            livesIndex = 0;
        }
        else if (lives <= 0)
        {
            Debug.Log("died bruh");
            skillCheck.enemyTurnToShoot = false;
            StartCoroutine(deathAnim());
        }
    }
    private IEnumerator deathAnim()
    {
        anim.SetBool("isDeadEnemy", true);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(deathAnim2());

    }
    private IEnumerator deathAnim2()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.8f);
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0).length);
        GameManager.RelocateAfterCombat = true;
        controller.searchScenes(GameManager.lastKnownScene); //but here later on call something from the game manager which stores the proper scene to go into
    }
    private IEnumerator enemyShootAndAnimation()
    {
        anim.SetTrigger("isShootEnemy");
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(enemyShootAndAnimation2());
    }

    private IEnumerator enemyShootAndAnimation2()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.6f);
        enemyFire();
    }
    private void enemyFire()
    {
        probOfLanding = Random.Range(0, 100);
        if (probOfLanding <= headShotRate)
        {
            Debug.Log("Player got head shotted");
            Shake.enemyShotShake();
            playerAnim.SetTrigger("isDamage");
            GameManager.Damage(damage * 2);
            if (GameManager.HealthCurrent > 0)
            {
                secondChanceTime = true;
                skillCheck.enemyTurnToShoot = false;
            }
        }
        else if (probOfLanding > headShotRate && probOfLanding <= probOfLandingTarget)
        {
            Debug.Log("Player got hit with a crippling shot");
            playerAnim.SetTrigger("isDamage");
            GameManager.Damage(damage);
            int bleedProbability = Random.Range(0, 100);
            if (bleedProbability <= bleedRate) // bleed controller
            {
                GameManager.bleeding = true;
            }
            else
            {
                bleedProbability = 0;
            }
            Shake.enemyShotShake(); //shakes the screen
            if (GameManager.HealthCurrent > 0)
            {
                secondChanceTime = true;
                skillCheck.enemyTurnToShoot = false;
            }
        }
        else
        {
            Debug.Log("Miss!");
            skillCheck.enemyTurnToShoot = false;
            secondChanceTime = true;
            enemyShootReset();
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


    

    private void enemyShootReset()
    {
        fireIndex = 0;
        probOfLanding = 0;
    }
}
