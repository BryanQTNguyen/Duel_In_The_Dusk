using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkScript : MonoBehaviour
{
    [SerializeField] SkillCheck skillCheck;

    public bool notAccurate = true;
    public int shotIndex;
    public bool playerShotAcc; //the player shot but did they do so accurately?
    // Start is called before the first frame update
    void Start()
    {
        notAccurate = true; 
        shotIndex = 0; //makes it so that the player only shoots like once
        playerShotAcc = false; //is the shot accurate
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
            skillCheck.playerShot = false;
            skillCheck.hideDraw();
            playerShotAcc = false;
            shotIndex=0;
            skillCheck.enemyTurnToShoot = true;
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
