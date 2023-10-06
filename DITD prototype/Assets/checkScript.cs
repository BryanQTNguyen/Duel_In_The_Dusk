using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkScript : MonoBehaviour
{
    [SerializeField] SkillCheck skillCheck;
    [SerializeField] GameObject missText;


    public bool notAccurate = true;
    public int shotIndex;
    public bool playerShotAcc; //the player shot but did they do so accurately?
    private bool timerMiss;
    private float timerMissValue;

    // Start is called before the first frame update
    void Start()
    {
        missText.SetActive(false);
        timerMiss = false;
        notAccurate = true; 
        shotIndex = 0; //makes it so that the player only shoots like once
        playerShotAcc = false; //is the shot accurate
        timerMissValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (notAccurate == false && skillCheck.playerShot == true && shotIndex ==0)
        {
            Debug.Log("Shot Landed!");
            skillCheck.hideDraw();
            playerShotAcc = true;
            shotIndex=1;
        }
        else if (notAccurate == true && skillCheck.playerShot == true && shotIndex ==0)
        {
            Debug.Log("Skill issue you missed");
            skillCheck.hideDraw();
            playerShotAcc = false;
            shotIndex=1;
            timerMiss = true;
        }
    }

    public void FixedUpdate()
    {
        if (timerMiss == true)
        {
            int i =0;
            timerMissValue = timerMissValue + Time.deltaTime;
            if(timerMissValue > 0.6f && i == 0)
            {
                missText.SetActive(true);
                i++;
            }
            if(timerMissValue > 1.6f && i == 1)
            {
                missText.SetActive(false);
                skillCheck.enemyTurnToShoot = true;
                timerMiss = false;
                timerMissValue = 0f;

            }

        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        notAccurate = false;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        notAccurate = true;
    }
}
