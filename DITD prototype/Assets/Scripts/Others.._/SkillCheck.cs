using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillCheck : MonoBehaviour
{
    public Slider mSlider;
    public bool index; //this is used to determine which direction the marker faces
    public float markerSpeed;
    public bool playerShot;
    public float playerAccuracy;
    public GameObject drawText;
    
    
    //varibles to control the draw function (timers mainly)
    private float timeDuration = 11f;
    public int drawTime;
    private float timer;
    private bool timerActive;


    [SerializeField] private CanvasGroup canvasGroup; //will also work as a variable which keeps track of when its draw time
    // Start is called before the first frame update
    void Start()
    {
        drawText.SetActive(false);
        drawTime = Random.Range(2, 10);
        canvasGroup.alpha = 0;
        mSlider.value = 100;
        playerShot = false;
        index = false;
        timerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive == true)
        {
            timer = timer + Time.deltaTime;
        }
        if(timer >= drawTime)
        {
            timerActive = false;
            Draw();
        }
        if (canvasGroup.alpha == 1)
        {
            if (Input.GetKeyDown("space")) //Player shoot
            {
                playerShot = true;
            }

            if (mSlider.value == mSlider.maxValue)
            {
                index = true;
            }
            if (mSlider.value == mSlider.minValue)
            {
                index = false;
            }
        }
    }
    void FixedUpdate()
    {
        if(playerShot == false && canvasGroup.alpha == 1)
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
        else if(playerShot == true)
        {
            playerAccuracy = mSlider.value;
            Debug.Log("Player Shot with an accuracy of: " + playerAccuracy);
        }

    }

    public void Draw()
    {
        drawText.SetActive(true);
        canvasGroup.alpha = 1;
    }

}