using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horseEndGame : MonoBehaviour
{
    [SerializeField] GameObject gameManagerObj;
    [SerializeField] gameManager GameManager;
    bool isInTalkingRange;
    [SerializeField] GameObject pressF;
    [SerializeField] SceneController sceneController;
    // Start is called before the first frame update
    void Start()
    {
        pressF.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        gameManagerObj = GameObject.Find("gameManager");
        GameManager = gameManagerObj.GetComponent<gameManager>();
        if(isInTalkingRange == true && GameManager.endGame == true)
        {
            if (Input.GetKeyDown("f"))
            {
                rideHorse();
            }
        }
    }

    private void rideHorse()
    {
        GameManager.lastFight = true;
        sceneController.lastFight();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && GameManager.endGame == true)
        {
            isInTalkingRange = true;
            pressF.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && GameManager.endGame == true)
        {
            isInTalkingRange = false;
            pressF.SetActive(false);
        }
    }
}
