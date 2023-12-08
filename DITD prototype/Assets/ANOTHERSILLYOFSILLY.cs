using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANOTHERSILLYOFSILLY : MonoBehaviour
{
    [SerializeField] GameObject gameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] ANOTHERSILLYOFSILLYtwo otherDoor;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        gameManagerObj = GameObject.Find("gameManager");
        GameManager = gameManagerObj.GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameManagerObj = GameObject.Find("gameManager");
        GameManager = gameManagerObj.GetComponent<gameManager>();
        if (GameManager.agroGame == true)
        {
            gameObject.SetActive(false);
            otherDoor.openDoor();
        }
    }
}
