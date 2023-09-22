using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkScript : MonoBehaviour
{
    public bool notAccurate = true;
    public SkillCheck skillCheck;
    public int shotIndex;
    public bool playerShotAcc; //the player shot but did they do so accurately?
    // Start is called before the first frame update
    void Start()
    {
        notAccurate = true;
        shotIndex = 0;
        playerShotAcc = false;
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
            skillCheck.timerReloadTime = true; 
            playerShotAcc = false;
            shotIndex=1;
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
