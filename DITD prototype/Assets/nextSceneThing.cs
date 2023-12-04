using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class nextSceneThing : MonoBehaviour
{
    [SerializeField] GameObject nextSceneCanvas;
    [SerializeField] GameObject TheGameManagerObj;
    [SerializeField] gameManager GameManager; 
    [SerializeField] DialgoueManager manager;

    public GameObject sceneObject;
    public SceneController controller;

    public string NextSceneNameSave;

    

    // Start is called before the first frame update
    void Start()
    {

        nextSceneCanvas.SetActive(false);
        sceneObject = GameObject.FindWithTag("sceneManager");
        controller = sceneObject.GetComponent<SceneController>();
    }


    // Update is called once per frame
    void Update()
    {
        TheGameManagerObj = GameObject.Find("gameManager");
        GameManager = TheGameManagerObj.GetComponent<gameManager>();
    }

    public void transitionOptions(string NextSceneName)
    {
        manager.isActive = true;
        nextSceneCanvas.SetActive(true);
        NextSceneNameSave = NextSceneName;
    }

    public void yes()
    {
        if (NextSceneNameSave == "SSToTS") //trainstation
            controller.trainStation();
        if (NextSceneNameSave == "TSToSS")
        {
            controller.outsideWorld();
            GameManager.SceneFrom = 2; 
        }


        if (NextSceneNameSave == "SSToS") //saloon
            controller.saloon();
        if(NextSceneNameSave == "SToSS")
        {
            controller.outsideWorld();
            GameManager.SceneFrom = 1;
        }

        if (NextSceneNameSave == "SSToB") //bank
            controller.bank();
        if (NextSceneNameSave == "BToSS")
        {
            controller.outsideWorld();
            GameManager.SceneFrom = 3;
        }

        if (NextSceneNameSave == "SSToBR") //barn
            controller.saloon();
        if (NextSceneNameSave == "BRToSS")
        {
            controller.outsideWorld();
            GameManager.SceneFrom = 4;
        }


        /*
        if (NextSceneNameSave == "SSToBr")
            controller.barn();
        */



    }
    public void no()
    {
        manager.isActive = false;
        nextSceneCanvas.SetActive(false);
    }
}
