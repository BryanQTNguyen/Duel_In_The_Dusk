using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
    public float enemyType;
    [SerializeField] DialgoueManager dialogueManager;
    [SerializeField] GameObject dialogueObject; 
    /*
     1 = deputy enemy 
     2 = ranger enemy 
     3 = cactus enemy 
     4 = banker boss
     5 = sheriff boss
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "cutSceneFirst" || SceneManager.GetActiveScene().name == "Train Station" || SceneManager.GetActiveScene().name
            == "Bank Interrior")
        {
            dialogueObject = GameObject.FindWithTag("dialogueManager");
            dialogueManager = dialogueObject.GetComponent<DialgoueManager>();
            if (dialogueManager != null)
            {


            }
            else
            {
                Debug.Log("Cannot find the Dialogue Manager (line 73)");
            }
        }
    }
}
