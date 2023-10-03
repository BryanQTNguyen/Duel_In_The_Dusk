using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShootProb : MonoBehaviour
{
    public float timer;
    [SerializeField] SkillCheck skillCheck; //reference to the skill check script
    [SerializeField] checkScript CheckScript; 
    public int probOfShooting;
    public int probOfLanding;
    [SerializeField] int fireIndex;
    public bool kill;
    [SerializeField] Animator anim;
    [SerializeField] shake Shake;
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
        if(skillCheck.enemyTurnToShoot == true && fireIndex == 0)
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
                Debug.Log("Gun Jammed");
                skillCheck.enemyTurnToShoot = false;

            }
        }

        if (CheckScript.playerShotAcc == true)
        {
            skillCheck.enemyTurnToShoot = false;
            anim.SetBool("isDeadEnemy", true);
        }

    }

    
    private void enemyFire()
    {
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
            Debug.Log("Miss!");
            skillCheck.enemyTurnToShoot = false;

        }
    }
   
}
