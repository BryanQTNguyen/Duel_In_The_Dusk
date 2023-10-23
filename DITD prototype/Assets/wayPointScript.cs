using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wayPointScript : MonoBehaviour
{
    float randomX;
    float randomY;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        RandomPos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RandomPos();
    }

    private void RandomPos()
    {
        randomX = Random.Range(-25.91405f, 37.06403f);
        randomY = Random.Range(-4.207f, 43.62354f);
        pos = new Vector2(randomX, randomY);
        transform.position = pos;
    }
}
