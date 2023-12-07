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
    [SerializeField] checkScript CheckScript;
    public Animator anim;
    public GameObject shotArea;
    [SerializeField] private CanvasGroup canvasGroup; //will also work as a variable which keeps track of when its draw time

    public bool index; //this is used to determine which direction the marker faces
    public int barIndex = 0;
    public float markerSpeed;
    public bool playerShot; // this is used to know if the player shot the gun
    public bool playerIsDead;
    public int ShotsToKill; //how many shots did the player take (3 will kill them)
    public float pressureLevel;
    
    //varibles to control the draw function (timers mainly)
    public int drawTime;
    private float timer;
    private bool timerActive;
    public float xPositionShot;

    public bool fireTime; //should be the same value as the canvas group this is made for other scripts to reference
    public int drawIndex = 0; //so the draw function doesn't run constantly

    public bool enemyTurnToShoot = false;



    // Start is called before the first frame update
    void Start()
    {
        drawText.SetActive(false);
        reloadingText.SetActive(false);
        drawTime = Random.Range(4, 10); //when the draw will show (randomly between 4 to 10 seconds)
        canvasGroup.alpha = 0;
        fireTime = false;
        barIndex = 0;
        mSlider.value = 0;
        playerShot = false; //did they shoot?
        index = false;
        timerActive = true;
        generateShotArea();
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
            EnemyShootProb.enemyStatSet();
        }
        if(timer >= drawTime)
        {
            timerActive = false;
            
            Draw();
        }
        if (canvasGroup.alpha == 1 || fireTime == true) //draw just happened
        {
            if (Input.GetKeyDown("space")) //Player shoot
            {
                playerShot = true;
                barIndex = 0;
                hideDraw();


                anim.SetTrigger("shot");
                Shake.playerRevolverShot();
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
        if (playerIsDead == false) //is player alive? only run when they are alive
        {
            if (canvasGroup.alpha == 1 || fireTime == true) //draw just happened
            {
                if (mSlider.value == mSlider.maxValue) //controls the movement of the marker
                {
                    index = true;
                }

                if (mSlider.value == mSlider.minValue)
                {
                    index = false;
                    barIndex++; //this index is used to control the count of how many times it had reached the min value

                }
                //controls it so that the player has a certain cycle of the bar to shoot
                if (barIndex == 3)
                {
                    Debug.Log("The time to shoot is over");
                    hideDraw();
                    enemyTurnToShoot = true;
                    //call some sort of boolean that will que the enemy to fire
                }
            }
        }
        if (playerShot == false && canvasGroup.alpha == 1 && playerIsDead == false) //this just moves the marker on the bar
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
        if(drawIndex == 0 && playerIsDead == false)
        {
            Debug.Log("hi i am draw");
            generateShotArea();
            drawText.SetActive(true);
            canvasGroup.alpha = 1;
            fireTime = true;
            drawIndex=1;
        }

    }

    public void hideDraw() //turns off the bar and marker mechanic (skill check)
    {
        Debug.Log("bye draw");
        canvasGroup.alpha = 0;
        fireTime = false;
        drawText.SetActive(false);
    }

    public void secondChance()
    {
        Debug.Log("hi i am working the second chance");
        mSlider.value = 0;
        barIndex = 0;
        drawIndex = 0;
        index = false;
        CheckScript.shotIndex = 0;
        playerShot = false;
        Draw();
    }

}