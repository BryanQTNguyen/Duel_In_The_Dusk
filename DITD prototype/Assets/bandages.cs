using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bandages : MonoBehaviour
{
    [SerializeField] GameObject gameManagerObj;
    [SerializeField] gameManager GameManager;
    public float amplitude = 0.1f;   // The amplitude of the movement
    public float frequency = 3.0f;
    private Vector3 startPosition;   // The initial position of the GameObject

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        gameObject.SetActive(true);
        gameManagerObj = GameObject.Find("gameManager");
        GameManager = gameManagerObj.GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPosition.y + amplitude * Mathf.Sin(frequency * Time.time);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(GameManager.bleeding == true || GameManager.HealthCurrent < 100)
            {
                GameManager.bleeding = false;
                GameManager.HealthCurrent = GameManager.HealthCurrent + 15;
                AudManager.Instance.PlaySFX("bandages");
                gameObject.SetActive(false);
            }
            else
            {

            }
        }
    }
}
