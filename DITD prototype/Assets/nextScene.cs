using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    [SerializeField] GameObject nextSceneCanvas;
    [SerializeField] DialgoueManager manager;
    public GameObject sceneObject;
    public SceneController controller;
    public bool SceneAdditive;
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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.isActive = true;
        nextSceneCanvas.SetActive(true);
    }

    public void yes()
    {
        if (SceneManager.GetActiveScene().name == "Train Station")
        {
            SceneAdditive = true;
            controller.outsideWorld();
        }

    }
    public void no()
    {
        manager.isActive = false;
        nextSceneCanvas.SetActive(false);
    }
}
