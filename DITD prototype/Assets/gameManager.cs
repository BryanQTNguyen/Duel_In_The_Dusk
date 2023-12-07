using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using System;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
    /*
    1 = deputy enemy 
    2 = ranger enemy 
    3 = cactus enemy 
    4 = banker boss
    5 = sheriff boss
    */


    [SerializeField] GameObject Character; //this is used to check the varibale damage is true
    [SerializeField] PlayerScript playerScript;

    [SerializeField] GameObject sceneControllerObject;
    [SerializeField] SceneController sceneController;

    //bleedStuff
    [SerializeField] GameObject BleedPanel;
    private float bleedTimer;
    public int bleedDamage = 2;
    public bool bleeding;


    //health stuff
    public int HealthCurrent;
    public int HealthMax = 100;
    [SerializeField] PlayerHealth healthBarScript;
    [SerializeField] GameObject healthBarObject;
    private Animator characterAnimCombat;


    //combat stuff
    public float enemyType;
    [SerializeField] GameObject Enemy;
    [SerializeField] Animator enemyAnimator;

    //after combat
    public string lastKnownScene;
    public Vector3 lastKnownPosition;

    //objective text and stuff
    [SerializeField] TMP_Text objectiveText;
    public int objectiveNumber;
    [SerializeField] GameObject objectWithPro;
    private string[] objectiveNames = { "Beat Cactus Tutorial (optional) or continue to saloon", "Find and rob the bank!", "Find a way out of the town",
    "You now hate the train go kill it", "Escape with horses"};

    //values for the location to transport the player

    //outside the saloon
    private float SaloonX = 67.22f;
    private float SaloonY = -1.25f;

    //outside the station
    private float StationX = 92.2f;
    private float StationY = -19.34f;

    //outside the bank
    private float BankX = 89.2f;
    private float BankY = 18.54f;

    public int SceneFrom;
    //From saloon = 1
    //From station = 2
    //From Bank = 3
    //From Barn = 4


    //Weapon variables 
    public int Ammo;
    public int gunType; // public variable made to inform other scripts that the player has this gun equipped

    public bool RelocateAfterCombat = false;



    /*
0 = revolver 
1 = rifle
2 = shotgun
*/


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        gunType = 0; //character starts with revolver
        Ammo = 6;
        healthBarObject = GameObject.FindWithTag("healthBar");
        healthBarScript = healthBarObject.GetComponent<PlayerHealth>();
        HealthCurrent = HealthMax;
        healthBarScript.SetMaxHealth(HealthMax);
    }

    // Update is called once per frame
    void Update()
    {
        relocatePlayer();

        if (HealthCurrent <= 0)
        {
            Death();
        }

        //this is used to initialize certain variables when the player is in
        if (SceneManager.GetActiveScene().name == "Train Station" || SceneManager.GetActiveScene().name
    == "Bank Interrior" || SceneManager.GetActiveScene().name == "Saloon" || SceneManager.GetActiveScene().name == "SampleScene" || 
    SceneManager.GetActiveScene().name == "Barn-stable" || SceneManager.GetActiveScene().name =="Combat") //checking if the scene is playabl
        {
            if (playerScript)
            {
                
            }
            else
            {
                Character = GameObject.FindWithTag("Player");
                playerScript = Character.GetComponent<PlayerScript>();
                healthBarObject = GameObject.FindWithTag("healthBar");
                healthBarScript = healthBarObject.GetComponent<PlayerHealth>();
                sceneControllerObject = GameObject.FindWithTag("sceneManager");
                sceneController = sceneControllerObject.GetComponent<SceneController>();
                BleedPanel = GameObject.Find("Canvas/bleedPanel");
                
            }
            
            if (playerScript != null)
            {
                Bleed();
            }
            else
            {
                Debug.Log("Cannot locate the player script");
            }

            if (HealthCurrent <= 0)
            {
                Death();
            }
            else
            {
                healthBarScript.SetHealth(HealthCurrent); //updating the healthBar
            }

        }
        else
        {
            Debug.Log("There is no player in this scene so I'm not loading"); //cannot find the proper script that identifies the player
        }

        //check enemy for combat scene
        if(enemyType != 0)
        {
            if(SceneManager.GetActiveScene().name == "Combat")
            {
                Enemy = GameObject.Find("MainEnemy");
                enemyAnimator = Enemy.GetComponent<Animator>();
                if(enemyType == 1)
                {
                    enemyAnimator.SetBool("deputy", true);

                    //cutely also resets all other booleans to be safe
                    enemyAnimator.SetBool("sheriff", false);
                    enemyAnimator.SetBool("cactus", false);
                    enemyAnimator.SetBool("banker", false);
                    enemyAnimator.SetBool("ranger", false);
                }
                if (enemyType == 2)
                {
                    enemyAnimator.SetBool("ranger", true);

                    //cutely also resets all other booleans to be safe
                    enemyAnimator.SetBool("sheriff", false);
                    enemyAnimator.SetBool("cactus", false);
                    enemyAnimator.SetBool("banker", false);
                    enemyAnimator.SetBool("deputy", false);
                }
                if (enemyType == 3)
                {
                    enemyAnimator.SetBool("cactus", true);

                    //cutely also resets all other booleans to be safe
                    enemyAnimator.SetBool("sheriff", false);
                    enemyAnimator.SetBool("deputy", false);
                    enemyAnimator.SetBool("banker", false);
                    enemyAnimator.SetBool("ranger", false);
                }
                if (enemyType == 4)
                {
                    enemyAnimator.SetBool("banker", true);

                    //cutely also resets all other booleans to be safe
                    enemyAnimator.SetBool("sheriff", false);
                    enemyAnimator.SetBool("cactus", false);
                    enemyAnimator.SetBool("deputy", false);
                    enemyAnimator.SetBool("ranger", false);
                }
                if (enemyType == 5)
                {
                    enemyAnimator.SetBool("sheriff", true);

                    //cutely also resets all other booleans to be safe
                    enemyAnimator.SetBool("deputy", false);
                    enemyAnimator.SetBool("cactus", false);
                    enemyAnimator.SetBool("banker", false);
                    enemyAnimator.SetBool("ranger", false);
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Combat")
        {
            Debug.Log("wtf no enemy type");
        }


        /*
        if (objectiveNumber == 0)
        {
            objectiveText.text = objectiveNames[0];
        }
        */

    }
    /*
    public void Damage() //damage function with a parameter for varying damages
    {
        if (playerScript.damageType == 1)
        {
            HealthCurrent = HealthCurrent - regularShot;
            playerScript.damageType = 0;
        }
        if (playerScript.damageType == 2)
        {
            HealthCurrent = 0;
            playerScript.damageType = 0;
        }
        if (playerScript.damageType == 3)
        {
            HealthCurrent = HealthCurrent - miniShot;
            playerScript.damageType = 0;
        }

    }
    */

    public void Damage(int damageAmount)
    {
        Debug.Log("daamge is worfdsjakl");
        HealthCurrent = HealthCurrent - damageAmount;
        healthBarObject = GameObject.FindWithTag("healthBar");
        healthBarScript = healthBarObject.GetComponent<PlayerHealth>();
        healthBarScript.SetHealth(HealthCurrent);
        if(HealthCurrent <= 0)
        {
            Death();
        }
    }

    public void Bleed()
    {
        if (playerScript.bleed == true)
        {
            BleedPanel.SetActive(true);
            bleedTimer += Time.deltaTime;
            if(bleedTimer >= 5)
            {
                HealthCurrent = HealthCurrent - bleedDamage;
                bleedTimer = 0;
            }
        }
        else
        {
            bleedTimer = 0;
            BleedPanel.SetActive(false);
        }
            
    }

    public void Death()
    {
        if(SceneManager.GetActiveScene().name == "Combat")
        {
            Character = GameObject.FindWithTag("Player");
            characterAnimCombat = Character.GetComponent<Animator>();
        }
        characterAnimCombat.SetTrigger("isDead");
        StartCoroutine(deathSceneTrans());
    }
    private IEnumerator deathSceneTrans()
    {
        yield return new WaitForSeconds(3.13f);
        sceneController.lose();
        Destroy(gameObject);
    }

    public void relocatePlayer()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene" && SceneFrom != 0)
        {
            if (SceneFrom == 1)
            {
                Character = GameObject.FindWithTag("Player");
                Character.transform.position = new Vector3(SaloonX, SaloonY, 0);
                SceneFrom = 0;
            }
        }
        if (SceneManager.GetActiveScene().name != "Combat" && RelocateAfterCombat == true)
        {
            Character = GameObject.FindWithTag("Player");
            Character.transform.position = lastKnownPosition;
            RelocateAfterCombat = false;
        }
    }
}
