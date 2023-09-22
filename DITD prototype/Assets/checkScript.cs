using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkScript : MonoBehaviour
{
    public bool notAccurate = true;
    public SkillCheck skillCheck;
    public int shotIndex;
    // Start is called before the first frame update
    void Start()
    {
        notAccurate = true;
        shotIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (notAccurate == false && skillCheck.playerShot == true && shotIndex ==0)
        {
            Debug.Log("Shot Landed!");
            shotIndex++;
        }
        else if (notAccurate == true && skillCheck.playerShot == true &&shotIndex ==0)
        {
            Debug.Log("Skill issue you missed");
            shotIndex++;
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
