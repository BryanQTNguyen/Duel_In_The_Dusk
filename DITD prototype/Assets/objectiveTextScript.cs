using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class objectiveTextScript : MonoBehaviour
{
    [SerializeField] GameObject gameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] TextMeshProUGUI objText;
    private string[] objTextValues = {"Find information about where my town's gold is at.", "Steal back the gold from the bank!", "Find a way to escape the town. Maybe by using a horse cause cool."};
    // Start is called before the first frame update
    void Start()
    {
        gameManagerObj = GameObject.Find("gameManager");
        GameManager = gameManagerObj.GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager == null)
        {
            gameManagerObj = GameObject.Find("gameManager");
            if(gameManagerObj != null)
                GameManager = gameManagerObj.GetComponent<gameManager>();
        }
        if (GameManager != null)
        {
            if(GameManager.earlyGame == true)
            {
                objText.text = objTextValues[0];
            }
            if(GameManager.earlyGameProgress == true)
            {
                objText.text = objTextValues[1];
            }
            if(GameManager.agroGame == true)
            {
                objText.text = objTextValues[2];
            }
        }
        
    }
}
