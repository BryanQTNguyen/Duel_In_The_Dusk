using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheck : MonoBehaviour
{
    public Slider mSlider;
    public bool index; //this is used to determine which direction the marker faces
    public float markerSpeed;
    public bool playerShot;
    public float playerAccuracy;
    [SerializeField] private CanvasGroup canvasGroup; //will also work as a variable which keeps track of when its draw time
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0;
        mSlider.value = 100;
        playerShot = false;
        index = false;
    }

    // Update is called once per frame
    void Update()
    {
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

}