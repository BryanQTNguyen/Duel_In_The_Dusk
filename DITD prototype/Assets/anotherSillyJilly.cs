using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anotherSillyJilly : MonoBehaviour
{
    [SerializeField] GameObject gameManagerObj;
    [SerializeField] gameManager GameManager;
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
        if (GameManager.agroGame == true)
        {
            gameObject.SetActive(false);
        }
    }
}
