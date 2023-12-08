using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OMGSILLYARROW : MonoBehaviour
{
    [SerializeField] GameObject gameManagerObj;
    [SerializeField] gameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
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
            gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.agroGame == true)
        {
            gameObject.SetActive(false);
            GameManager.hasKey = true;

        }
    }

}
