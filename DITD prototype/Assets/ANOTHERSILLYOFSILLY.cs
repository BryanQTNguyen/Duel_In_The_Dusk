using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ANOTHERSILLYOFSILLY : MonoBehaviour
{
    [SerializeField] GameObject gameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] ANOTHERSILLYOFSILLYtwo otherDoor;
    private bool timerForText;
    [SerializeField] GameObject textStuff;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        gameManagerObj = GameObject.Find("gameManager");
        GameManager = gameManagerObj.GetComponent<gameManager>();
        textStuff.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        gameManagerObj = GameObject.Find("gameManager");
        GameManager = gameManagerObj.GetComponent<gameManager>();

        if(timerForText == true)
        {
            timer = timer + Time.deltaTime;
            if (timer >= 3f)
            {
                textStuff.SetActive(false);
                timerForText = false;
                timer = 0;
                timerForText = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.agroGame == true && GameManager.hasKey == false)
        {
            textStuff.SetActive(true);
            timerForText = true;
        }
        if (GameManager.hasKey == true)
        {
            gameObject.SetActive(false);
            otherDoor.openDoor();
        }
    }
}
