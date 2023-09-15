using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    public bool isInTalkingRange = false;
    public DialgoueManager manager;
    public FadeScript fadeScript;
    public GameObject pressF;
    public string[] audios; 

    private void Start()
    {
        pressF.SetActive(false);
    }
    public void StartDialogue()
    {
        FindObjectOfType<DialgoueManager>().OpenDialogue(messages, actors, audios);
    }

    void Update()
    {
        if (Input.GetKeyDown("f") && isInTalkingRange && manager.isActive == false && fadeScript.doneFadingOut == true)
        {
            StartDialogue();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        isInTalkingRange = true;
        pressF.SetActive(true);

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        isInTalkingRange = false;
        pressF.SetActive(false);

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
