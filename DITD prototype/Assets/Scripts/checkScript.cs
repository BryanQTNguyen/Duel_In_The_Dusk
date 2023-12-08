using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkScript : MonoBehaviour
{
    [SerializeField] SkillCheck skillCheck;
    [SerializeField] GameObject missText;
    [SerializeField] enemyShootProb EnemyShootProb;


    public bool notAccurate = true;
    public int shotIndex;
    public bool playerShotAcc; //the player shot but did they do so accurately?
    private bool timerMiss;
    private float timerMissValue;
    int i = 0;

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
        if(skillCheck.playerShot == true && shotIndex == 0)
        {
            if (notAccurate == false)
            {
                AudManager.Instance.PlaySFX("playerShot");
                Debug.Log("Shot Landed!"); //player accuracy descr
                playerShotAcc = true;
                shotIndex = 1;
            }
            else if (notAccurate == true)
            {
                AudManager.Instance.PlaySFX("playerShot");
                Debug.Log("Skill issue you missed");
                skillCheck.playerShot = false;
                playerShotAcc = false;
                shotIndex++;
                timerMiss = true;
            }
        }
    }

    public void FixedUpdate()
    {
        if (timerMiss == true)
        {
            timerMissValue = timerMissValue + Time.deltaTime;
            if(timerMissValue > 0.6f && i == 0)
            {
                missText.SetActive(true);
                i++;
            }
            if(timerMissValue > 1.3f && i == 1)
            {
                missText.SetActive(false);
                EnemyShootProb.fireIndex = 0;
                timerMiss = false;
                timerMissValue = 0f;
                i = 0;
                StartCoroutine(enemyTurnToShoot());
            }

        }
        
    }
    private IEnumerator enemyTurnToShoot()
    {
        yield return new WaitForSeconds(0.5f);
        skillCheck.enemyTurnToShoot = true;
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
