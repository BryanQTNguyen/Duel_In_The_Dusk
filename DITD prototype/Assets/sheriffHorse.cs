using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class sheriffHorse : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the movement
    private bool moveHorseBool = false;
    private GameObject managerObj;
    private gameManager manager;
    private GameObject character;
    private bool muteDialogueAudio;
    public SceneController controller;

    private void Start()
    {
        StartCoroutine(horseApproach());
        moveHorseBool = false;
    }
    private IEnumerator horseApproach()
    {
        yield return new WaitForSeconds(5f);
        moveHorse();
    }

    private void moveHorse()
    {
        moveHorseBool = true;
    }

    void Update()
    {
        if(moveHorseBool == true)
        {
            // Move the GameObject upwards
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            // Check if the GameObject has reached the center of the screen
            if (transform.position.y >= 0.64)
            {
                // Stop moving when it reaches the center
                moveHorseBool = false;
                StartCoroutine(finalBattle());
                
            }
        }

    }
    private IEnumerator finalBattle()
    {
        yield return new WaitForSeconds(0.5f);
        managerObj = GameObject.Find("gameManager");
        manager = managerObj.GetComponent<gameManager>();
        manager.enemyType = 5f;
        manager.enemyName = "sheriff";
        manager.lastKnownScene = "lastFight";
        character = GameObject.Find("Carter");
        manager.lastKnownPosition = character.transform.position;
        muteDialogueAudio = true;
        manager.isLastFight = true;
        controller.combat();
    }
}