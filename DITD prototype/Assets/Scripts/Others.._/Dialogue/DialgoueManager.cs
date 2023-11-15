using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialgoueManager : MonoBehaviour
{
    public Image actorImage;
    public TMP_Text actorName;
    public string AudioToPlay;
    public TMP_Text messageText;
    public RectTransform backgroundBox;
    public AudioSource audioSource;
    [SerializeField] private DialogueTrigger dialogueTrigger; 

    Message[] currentMessages;
    Actor[] currentActor;
    string[] currentAudio;
    int activeMessage = 0;
    public bool isActive = false;


    public FadeScript fadeScript;

    public void OpenDialogue(Message[] messages, Actor[] actors, string[] audios)
    {
        fadeScript.ShowDialogue();
        currentMessages = messages;
        currentActor = actors;
        currentAudio = audios;
        activeMessage = 0;
        isActive = true;
        DisplayMessage();
    }

    void DisplayMessage()
    {
        audioSource.volume = 1;
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        AudioToPlay = currentAudio[activeMessage];
        AudManager.Instance.PlayDialogue(AudioToPlay);
        
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
            if (dialogueTrigger.isCutScene == false)
            {
                isActive = false;
                fadeScript.HideDialogueFade();
                audioSource.volume = 0;
            }
            else
            {
                //show button to continue to next scene
            }

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
