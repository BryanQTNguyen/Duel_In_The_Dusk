using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool isCutScene;
    public Message[] messages;
    public Actor[] actors;
    public bool isInTalkingRange = false;
    public DialgoueManager manager;
    public FadeScript fadeScript;
    public GameObject pressF;
    public string[] audios;
    public bool agressiveStart; // this is for auto dialogue appear if you get close
    private int index = 0; // this index is used to make sure aggressive dialogue doesn't happen multiple times
    private int indexTwo = 0; //this index is used to make sure the cutscene start dialogue doesn't happen multiple times

    private void Start()
    {
        if(isCutScene == false)
        {
            pressF.SetActive(false);
        }
    }
    public void StartDialogue()
    {
        FindObjectOfType<DialgoueManager>().OpenDialogue(messages, actors, audios);
    }

    void Update()
    {
        if (isCutScene == true && indexTwo == 0)
        {
            StartDialogue();
            indexTwo = 1; 
        }
        if(isCutScene == false)
        {
            if (Input.GetKeyDown("f") && isInTalkingRange && manager.isActive == false && fadeScript.doneFadingOut == true && agressiveStart == false)
            {
                StartDialogue();
            }
            if (isInTalkingRange == true && agressiveStart == true && index == 0)
            {
                StartDialogue();
                index = 1;
            }
        }

        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        isInTalkingRange = true;
        if(agressiveStart == false)
            pressF.SetActive(true);

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        isInTalkingRange = false;
        index = 0;
        if (agressiveStart == false)
            pressF.SetActive(true);

    }
}

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
