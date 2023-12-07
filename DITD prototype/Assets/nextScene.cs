using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class nextScene : MonoBehaviour
{
    public string nextSceneName;

    [SerializeField] GameObject nextSceneThingObj;
    [SerializeField] nextSceneThing NextSceneThing;
    public void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        nextSceneName = gameObject.name;
        NextSceneThing.transitionOptions(nextSceneName);
        Debug.Log(nextSceneName);
    }
}
