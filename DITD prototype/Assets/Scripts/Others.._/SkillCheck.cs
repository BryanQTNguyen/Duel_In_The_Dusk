using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;

public class SkillCheck : MonoBehaviour
{
    // objects referenced from the unity project
    public GameObject drawText;
    public GameObject reloadingText;
    public Slider mSlider;
    [SerializeField] private enemyShootProb EnemyShootProb;
    [SerializeField] shake Shake;
    public Animator anim;
    public GameObject shotArea;
    public checkScript CheckScript;
    [SerializeField] private CanvasGroup canvasGroup; //will also work as a variable which keeps track of when its draw time


    public bool index; //this is used to determine which direction the marker faces
    public int barIndex = 0;
    public float markerSpeed;
    public bool playerShot; // this is used to know if the player shot the gun
    public bool playerIsDead;
    public int ShotsToKill; //how many shots did the player take (3 will kill them)
    
    //varibles to control the draw function (timers mainly)
    public int drawTime;
    private float timer;
    private bool timerActive;
    public float xPositionShot;

    public bool fireTime; //should be the same value as the canvas group this is made for other scripts to reference
    public int drawIndex = 0; //so the draw function doesn't run constantly

    //variables and etc for reload function
    public bool timerReloadTime;
    float timerReload;


    public bool enemyTurnToShoot = false;


    // Start is called before the first frame update
    void Start()
    {
        drawText.SetActive(false);
        reloadingText.SetActive(false);
        drawTime = Random.Range(2, 10);
        canvasGroup.alpha = 0;
        fireTime = false;
        mSlider.value = 0;
        playerShot = false; //did they shoot?
        index = false;
        timerActive = true;
        generateShotArea();
        timerReloadTime = false;
        playerIsDead = false;


    }

    public void generateShotArea() //controls shoot area (orange thing)
    {
        xPositionShot = Random.Range(-88.7f, 88.518f);
        shotArea.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPositionShot, 0.8536f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive == true) //controls when the draw will happen
        {
            timer = timer + Time.deltaTime;
        }
        if(timer >= drawTime)
        {
            timerActive = false;
            Draw();
        }
        


        if (playerIsDead == false) //is player alive? only run when they are alive
        {
            if (canvasGroup.alpha == 1 || fireTime == true) //draw just happened
            {
                if (Input.GetKeyDown("space")) //Player shoot
                {
                    playerShot = true;
                    anim.SetTrigger("shot");
                    Shake.playerRevolverShot();
                }

                if (mSlider.value == mSlider.maxValue && fireTime == true) //controls the movement of the marker
                {
                    index = true;
                }
                if (mSlider.value == mSlider.minValue && fireTime == true)
                {
                    index = false;
                    barIndex++; //this index is used to control the count of how many times it had reached the min value
                    Debug.Log("I am part of the problem");

                }

                //controls it so that the player has a certain cycle of the bar to shoot
                if (barIndex == 2)
                {
                    hideDraw();
                    enemyTurnToShoot = true;
                    Debug.Log("I am the problem");
                    //call some sort of boolean that will que the enemy to fire
                }
            }
        }


        /*
        controls the main reload mechanic
        if (timerReloadTime == true && playerIsDead == false)
        {
            timerReload = timerReload + Time.deltaTime;
            reloadingText.SetActive(true);
            if (timerReload >= 1.3)
            {
                reloadingText.SetActive(false);
                drawIndex = 0;
                Draw();
                timerReload = 0;
                timerReloadTime = false;
                playerShot = false;
                CheckScript.shotIndex = 0;
            }
        }
        */
    }
    void FixedUpdate()
    {
        if(playerShot == false && canvasGroup.alpha == 1 && playerIsDead == false) //this just moves the marker on the bar
        {
            if (index == false)
            {
                mSlider.value += markerSpeed;
            }
            else if (index == true)
            {
                mSlider.value -= markerSpeed;
            }
        }
    }

    public void Draw() //turns on the bar and marker mechanic (skill check)
    {
        if(drawIndex == 0)
        {
            drawText.SetActive(true);
            canvasGroup.alpha = 1;
            fireTime = true;
            drawIndex=1;
        }

    }

    public void hideDraw() //turns off the bar and marker mechanic (skill check)
    {
        drawText.SetActive(false);
        canvasGroup.alpha = 0;
        fireTime = false;
    }

    public void PlayerDamage() //damage mechanic
    {
        if (EnemyShootProb.kill == true || ShotsToKill == 3)
        {
            hideDraw();
            anim.SetBool("isDead", true);
            playerIsDead = true;
            Debug.Log("Player Has fallen");
        }
        else if(EnemyShootProb.kill == false)
        {
            //player got damaged
            anim.SetTrigger("isDamage");
            ShotsToKill++;
            if(ShotsToKill == 3)
            {
                PlayerDamage();
            }
        }

    }

    public void secondChance()
    {
        mSlider.value = 0;
        barIndex = 0;
        drawIndex = 0;
        Draw();
    }

}