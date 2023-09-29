using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShootProb : MonoBehaviour
{
    public float timer;
    public SkillCheck skillCheck; //reference to the skill check script
    public checkScript CheckScript; 
    public int probOfShooting;
    public int probOfLanding;
    [SerializeField] private int fireIndex;
    public bool kill;
    [SerializeField] private Animator anim;
    [SerializeField] private shake Shake;
    public float waitUntilNextShot;
    // Start is called before the first frame update
    void Start()
    {
        fireIndex = 0;
        waitUntilNextShot = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckScript.playerShotAcc == false)
        {
            if ((skillCheck.timerReloadTime || skillCheck.fireTime == true) && fireIndex == 0)
            {
                timer = timer + Time.deltaTime;
                if (timer > waitUntilNextShot)
                {
                    probOfShooting = Random.Range(0, 100);
                    if (probOfShooting < 70 && timer > waitUntilNextShot)
                    {
                        anim.SetTrigger("isShootEnemy");
                        enemyFire();
                    }
                    else if (probOfShooting > 70 && timer > waitUntilNextShot)
                    {
                        fireIndex = 0;
                        timer = 0f;
                        Debug.Log("Enemy did not decide to shoot this time, maybe soon");
                    }
                }
            }
        }
        if(CheckScript.playerShotAcc == true)
        {
            anim.SetBool("isDeadEnemy", true);
        }

    }

    
    private void enemyFire()
    {
        timer = 0f;
        fireIndex++;
        probOfLanding = Random.Range(0, 100);
        if (probOfLanding <= 10)
        {
            Debug.Log("Player got head shotted");
            Shake.enemyShotShake();
            kill = true;
            skillCheck.PlayerDamage();

        }
        else if(probOfLanding > 10 && probOfLanding <= 70)
        {
            fireIndex = 0;
            Debug.Log("Player got hit with a crippling shot");
            Shake.enemyShotShake();
            kill = false; 
            skillCheck.PlayerDamage();
        }
        else
        {
            timer = 0f;
            fireIndex = 0;
            Debug.Log("Miss!");
        }
    }
   
}
