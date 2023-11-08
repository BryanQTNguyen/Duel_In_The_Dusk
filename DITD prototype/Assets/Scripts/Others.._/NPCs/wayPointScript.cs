using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wayPointScript : MonoBehaviour
{
    float randomX;
    float randomY;
    Vector3 pos;
    public bool roamOne;
    public bool roamTwo;
    public bool roamThree;
    public bool roamFour;
    public bool roamFive;
    // Start is called before the first frame update
    void Start()
    {
        RandomPos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "staticObject")
        {
            RandomPos();
        }
        if(collision.gameObject.tag == "npc")
        {
            RandomPos();
        }
    }

    public void RandomPos()
    {
        if (roamOne)
        {
            randomX = Random.Range(36.431f, 92.8f);
            randomY = Random.Range(-21.051f, 17.64f);
        }
        if (roamTwo)
        {
            randomX = Random.Range(36.431f, 92.8f);
            randomY = Random.Range(-21.051f, 17.64f);
        }
        if (roamThree)
        {
            randomX = Random.Range(36.431f, 92.8f);
            randomY = Random.Range(-21.051f, 17.64f);
        }
        if (roamFour)
        {
            randomX = Random.Range(36.431f, 92.8f);
            randomY = Random.Range(-21.051f, 17.64f);
        }
        if (roamFive)
        {
            randomX = Random.Range(36.431f, 92.8f);
            randomY = Random.Range(-21.051f, 17.64f);
        }
        
        pos = new Vector3(randomX, randomY, 0);
        transform.position = pos;
    }
}
