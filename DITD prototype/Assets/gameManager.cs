using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
    public float enemyType;
    /*
    1 = deputy enemy 
    2 = ranger enemy 
    3 = cactus enemy 
    4 = banker boss
    5 = sheriff boss
    */


    [SerializeField] GameObject Character; //this is used to check the varibale damage is true
    [SerializeField] PlayerScript playerScript;

    //health stuff
    public int HealthCurrent;
    public int HealthMax = 100;
    public PlayerHealth healthBar;

    //damage stuff
    public int regularShot = 33;
    public int instantKill = 100;
    public int bleedDamage = 2;
    public int miniShot = 10;

    public Rigidbody2D rb;

    //Weapon variables 
    public int Ammo;
    public int gunType; // public variable made to inform other scripts that the player has this gun equipped
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
        gunType = 0;
        Ammo = 6; //character starts with revolver
        HealthCurrent = HealthMax;
        healthBar.SetMaxHealth(HealthMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthCurrent <= 0)
        {
            Death();
        }
        /* health bar test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(10);
        }
        */
        if (SceneManager.GetActiveScene().name == "Train Station" || SceneManager.GetActiveScene().name
            == "Bank Interrior") //checking if the scene is playable
        {
            Character = GameObject.FindWithTag("Player");
            playerScript = Character.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                Damage();
                Bleed();
            }
            else
            {
                Debug.Log("Cannot locate the player");
            }
        }
    }
    public void Damage() //damage function with a parameter for varying damages
    {
        if (playerScript.damageType == 1)
            HealthCurrent = HealthCurrent - regularShot;
        if (playerScript.damageType == 2)
            HealthCurrent = HealthCurrent - instantKill;
        if (playerScript.damageType == 3)
            HealthCurrent = HealthCurrent - miniShot;
        
    }
    public void Bleed()
    {
        if (playerScript.bleed == true)
            HealthCurrent = HealthCurrent - bleedDamage;
    }

    public void Death()
    {
        //load ending screen
    }
}
