using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialgoueManager : MonoBehaviour
{
    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    Actor[] currentActor;
    int activeMessage = 0;
    public bool isActive = false;

    public FadeScript fadeScript;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        fadeScript.ShowDialogue();
        currentMessages = messages;
        currentActor = actors;
        activeMessage = 0;
        isActive = true;
        DisplayMessage();
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActor[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }
    public void NextMessage()
    {
        activeMessage++;
        if(activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            isActive = false;
            fadeScript.HideDialogueFade();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        fadeScript.HideDialogue();
    }

// Update is called once per frame
void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive == true && fadeScript.doneFadingIn == true)
        {
            NextMessage();
        }
    }
}
