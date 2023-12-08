using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialgoueManager : MonoBehaviour
{
    //All the variables for the displaying dialgoue 
    public Image actorImage;
    public TMP_Text actorName;
    public string AudioToPlay;
    public TMP_Text messageText;
    public RectTransform backgroundBox;

    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private GameObject continueButton;

    //combat stuff
    [SerializeField] GameObject managerObj;
    [SerializeField] gameManager manager;
    [SerializeField] SceneController controller;
    public float privateEnemyType;
    [SerializeField] GameObject character;


    public bool muteDialogueAudio;
    public bool callToCombatInGameManager;
    Message[] currentMessages;
    Actor[] currentActor;
    string[] currentAudio;
    int currentEnemy;
    string currentEnemySpecifics;
    int activeMessage = 0;
    public bool isActive = false;
    public float typingSpeed;


    public FadeScript fadeScript;

    public void OpenDialogue(Message[] messages, Actor[] actors, string[] audios, int enemyIdentify, string enemyName)
    {
        fadeScript.ShowDialogue();
        currentMessages = messages;
        currentActor = actors;
        currentAudio = audios;
        currentEnemy = enemyIdentify;
        currentEnemySpecifics = enemyName;
        activeMessage = 0;
        isActive = true;
        DisplayMessage();
    }


    void DisplayMessage()
    {
        muteDialogueAudio = false;
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
            if (currentEnemy == 1 || currentEnemy == 2 || currentEnemy == 3 || currentEnemy == 4|| currentEnemy == 5)
            {
                managerObj = GameObject.Find("gameManager");
                manager = managerObj.GetComponent<gameManager>();
                manager.enemyType = currentEnemy;
                manager.enemyName = currentEnemySpecifics;
                manager.lastKnownScene = SceneManager.GetActiveScene().name;
                character = GameObject.Find("Carter");
                manager.lastKnownPosition = character.transform.position;
                muteDialogueAudio = true;
                controller.combat();
            }else if (dialogueTrigger.isCutScene == false)
            {
                isActive = false;
                fadeScript.HideDialogueFade();
                muteDialogueAudio = true;

            }else if(dialogueTrigger.isCutScene == true)
            {
                muteDialogueAudio = true;
                AudManager.Instance.PlayDialogue("Silence");
                continueButton.gameObject.SetActive(true);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        fadeScript.HideDialogue();
        if (dialogueTrigger.isCutScene == true)
        {
            continueButton.gameObject.SetActive(false);
        }
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
