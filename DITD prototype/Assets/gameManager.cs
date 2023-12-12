using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

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
    public string enemyName;
    [SerializeField] GameObject Enemy;
    [SerializeField] Animator enemyAnimator;
    private GameObject backgroundAnimatorObj;
    [SerializeField] Animator backgroundAnimator;
    public bool isLastFight;

    //after combat
    public string lastKnownScene;
    public Vector3 lastKnownPosition;
    public GameObject enemyToDeactivate;
    public List<string> enemiesToDeactivateBank = new List<string>();
    public List<string> enemiesToDeactivateSS = new List<string>();
    public List<string> enemiesToDeactivateBarn = new List<string>();

    //objective text and stuff
    [SerializeField] TMP_Text objectiveText;
    public int objectiveNumber;
    [SerializeField] GameObject objectWithPro;
    private string[] objectiveNames = { "Beat Cactus Tutorial (optional) or continue to saloon", "Find and rob the bank!", "Find a way out of the town",
    "You now hate the train go kill it", "Escape with horses"};

    public bool hasKey = false;

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

    //outside the barn
    private float barnX = 45.78f;
    private float barnY = 19.39f;

    public int SceneFrom;

    //From saloon = 1
    //From station = 2
    //From Bank = 3
    //From Barn = 4

    //different types of enemies and stuff
    [SerializeField] GameObject deputy1;
    [SerializeField] GameObject deputy2;
    [SerializeField] GameObject deputy3;
    [SerializeField] GameObject ranger4;
    [SerializeField] GameObject ranger5;
    [SerializeField] GameObject banker;
    [SerializeField] GameObject sheriff;
    [SerializeField] GameObject deputyBank;
    [SerializeField] GameObject rangerBank;

    //story variables
    public bool earlyGame = true; //where nothing happened yet just silly
    public bool earlyGameProgress = false; //leave saloon
    public bool agroGame = false; //just robbed bank
    public bool endGame = false; //got to the horses
    public bool lastFight = false;
    [SerializeField] GameObject arrowKey;




    public bool RelocateAfterCombat = false;



    /*
0 = revolver 
1 = rifle
2 = shotgun
*/
    //reminder
    public GameObject textStuff;
    private bool timerForText;
    private float timer;

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
        earlyGame = true;
        earlyGameProgress = false;
        agroGame = false;
        endGame = false;
        lastFight = false;
        hasKey = false;
        isLastFight = false;
        healthBarObject = GameObject.FindWithTag("healthBar");
        healthBarScript = healthBarObject.GetComponent<PlayerHealth>();
        HealthCurrent = HealthMax;
        healthBarScript.SetMaxHealth(HealthMax);
    }

    // Update is called once per frame
    void Update()
    {
        healthBarScript.SetHealth(HealthCurrent);

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if(arrowKey == null)
                arrowKey = GameObject.Find("arrow");
            if (agroGame == true && hasKey == false)
            {
                arrowKey.SetActive(true);
            }
            else if(arrowKey.active== true)
            {
                arrowKey.SetActive(false);
            }
        }
        relocatePlayer();

        if (HealthCurrent <= 0)
        {
            Death();
        }
      
        if (SceneManager.GetActiveScene().name == "Bank Interrior" && enemiesToDeactivateBank.Count >= 1)
        {
            foreach (string enemyNames in enemiesToDeactivateBank)
            {
                // Find the GameObject by name
                GameObject gameObjectToDeactivate = GameObject.Find(enemyNames);

                // Check if the GameObject is found
                if (gameObjectToDeactivate != null)
                {
                    // Deactivate the GameObject
                    gameObjectToDeactivate.SetActive(false);
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Barn-stable" && enemiesToDeactivateBarn.Count >= 1)
        {
            foreach (string enemyNames in enemiesToDeactivateBarn)
            {
                // Find the GameObject by name
                GameObject gameObjectToDeactivate = GameObject.Find(enemyNames);

                // Check if the GameObject is found
                if (gameObjectToDeactivate != null)
                {
                    // Deactivate the GameObject
                    gameObjectToDeactivate.SetActive(false);
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "SampleScene" && enemiesToDeactivateSS.Count >= 1)
        {
            foreach (string enemyNames2 in enemiesToDeactivateSS)
            {
                // Find the GameObject by name
                GameObject gameObjectToDeactivate2 = GameObject.Find(enemyNames2);

                // Check if the GameObject is found
                if (gameObjectToDeactivate2 != null)
                {
                    // Deactivate the GameObject
                    gameObjectToDeactivate2.SetActive(false);
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Sample Scene" && enemiesToDeactivateBarn.Count >= 1)
        {
            foreach (string enemyNames3 in enemiesToDeactivateBarn)
            {
                // Find the GameObject by name
                GameObject gameObjectToDeactivate3 = GameObject.Find(enemyNames3);

                // Check if the GameObject is found
                if (gameObjectToDeactivate3 != null)
                {
                    // Deactivate the GameObject
                    gameObjectToDeactivate3.SetActive(false);
                }
            }
        }
        //this is used to initialize certain variables when the player is in
        if (SceneManager.GetActiveScene().name == "Train Station" || SceneManager.GetActiveScene().name
    == "Bank Interrior" || SceneManager.GetActiveScene().name == "Saloon" || SceneManager.GetActiveScene().name == "SampleScene" || 
    SceneManager.GetActiveScene().name == "Barn-stable" || SceneManager.GetActiveScene().name =="Combat" || SceneManager.GetActiveScene().name == "lastFight") //checking if the scene is playabl
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

            if (SceneManager.GetActiveScene().name == "SampleScene" && agroGame == false)
            {
                if(deputy1 == null)
                {
                    Debug.Log("hhioifdjspijfds");
                    deputy1 = GameObject.Find("deputy1");
                    deputy2 = GameObject.Find("deputy2");
                    deputy3 = GameObject.Find("deputy3");
                    ranger4 = GameObject.Find("ranger4");
                    ranger5 = GameObject.Find("ranger5");

                }
                if (deputy1.active == true)
                {
                    Debug.Log("dfsfds");
                    deputy1.SetActive(false);
                    deputy2.SetActive(false);
                    deputy3.SetActive(false);
                    ranger4.SetActive(false);
                    ranger5.SetActive(false);
                }


            }else if (SceneManager.GetActiveScene().name == "Bank Interrior" && earlyGame == true)
            {
                if (deputyBank == null)
                {
                    deputyBank = GameObject.Find("deputyBank");
                }
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
                backgroundAnimatorObj = GameObject.Find("interiorArt");
                backgroundAnimator = backgroundAnimatorObj.GetComponent<Animator>();
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
                if (lastKnownScene == "SampleScene")
                {
                    foreach (AnimatorControllerParameter parameter in backgroundAnimator.parameters)
                    {
                        backgroundAnimator.SetBool(parameter.name, false);
                    }
                    backgroundAnimator.SetBool("outside", true);
                    


                }
                if(lastKnownScene == "lastFight")
                {
                    foreach (AnimatorControllerParameter parameter in backgroundAnimator.parameters)
                    {
                        backgroundAnimator.SetBool(parameter.name, false);
                    }
                    backgroundAnimator.SetBool("outside", true);
                }
                if (lastKnownScene == "Saloon")
                {
                    foreach (AnimatorControllerParameter parameter in backgroundAnimator.parameters)
                    {
                        backgroundAnimator.SetBool(parameter.name, false);
                    }
                    backgroundAnimator.SetBool("saloon", true);
                    
                }
                if (lastKnownScene == "Train Station")
                {
                    foreach (AnimatorControllerParameter parameter in backgroundAnimator.parameters)
                    {
                        backgroundAnimator.SetBool(parameter.name, false);
                    }
                    backgroundAnimator.SetBool("trainStation", true);
                    
                }
                if (lastKnownScene == "Barn-stable")
                {
                    foreach (AnimatorControllerParameter parameter in backgroundAnimator.parameters)
                    {
                        backgroundAnimator.SetBool(parameter.name, false);
                    }
                    backgroundAnimator.SetBool("barn", true);
                    
                }
                if (lastKnownScene == "Bank Interrior")
                {
                    foreach (AnimatorControllerParameter parameter in backgroundAnimator.parameters)
                    {
                        backgroundAnimator.SetBool(parameter.name, false);
                    }
                    backgroundAnimator.SetBool("bank", true);
                    
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
        if (bleeding == true)
        {
            BleedPanel.SetActive(true);
            bleedTimer += Time.deltaTime;
            if(bleedTimer >= 5)
            {
                HealthCurrent = HealthCurrent - bleedDamage;
                AudManager.Instance.PlaySFX("playerHit");
                bleedTimer = 0;
            }
        }
        else
        {
            bleedTimer = 0;
            BleedPanel.SetActive(false);
        }
        if(HealthCurrent <= 0)
        {
            Death();
        }
            
    }

    public void Death()
    {
        if(SceneManager.GetActiveScene().name == "Combat")
        {
            Character = GameObject.FindWithTag("Player");
            characterAnimCombat = Character.GetComponent<Animator>();
            characterAnimCombat.SetTrigger("isDead");
        }
        StartCoroutine(deathSceneTrans());
    }
    private IEnumerator deathSceneTrans()
    {
        yield return new WaitForSeconds(3.13f);
        sceneController.lose();
        bleeding = false;
        Debug.Log("hi");
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
            if(SceneFrom == 3)
            {
                Character = GameObject.FindWithTag("Player");
                Character.transform.position = new Vector3(BankX, BankY, 0);
                
                SceneFrom = 0;
                
            }
            if (SceneFrom == 4)
            {
                Character = GameObject.FindWithTag("Player");
                Character.transform.position = new Vector3(barnX, barnY, 0);
                SceneFrom = 0;
            }
        }
        if (SceneManager.GetActiveScene().name != "Combat" && RelocateAfterCombat == true)
        {
            if (isLastFight == true)
            {
                Debug.Log("hi");
                bleeding = false;
                Destroy(gameObject);
            }
            if(lastFight== false)
            {
                Character = GameObject.FindWithTag("Player");
                Character.transform.position = lastKnownPosition;

                if (enemyName != "cactus")
                {
                    enemyToDeactivate = GameObject.Find(enemyName);
                    if (lastKnownScene == "Bank Interrior")
                    {
                        enemiesToDeactivateBank.Add(enemyName);
                    }
                    if (lastKnownScene == "SampleScene")
                    {
                        enemiesToDeactivateSS.Add(enemyName);
                    }
                    if (lastKnownScene == "Barn-stable")
                    {
                        enemiesToDeactivateBarn.Add(enemyName);
                    }
                    enemyToDeactivate.SetActive(false);
                }
                if (enemyName == "banker")
                {
                    agroGame = true;
                }
                if (enemyName == "rangerBank")
                {
                    endGame = true;
                }
                Character.transform.position = lastKnownPosition;

                RelocateAfterCombat = false;
            }
                
            
        }
            
    }
}
