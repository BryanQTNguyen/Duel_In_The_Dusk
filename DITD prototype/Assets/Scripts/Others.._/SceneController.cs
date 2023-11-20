using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public Animator transitionAnim;
    public int sceneID;
    [SerializeField] nextScene NextScene;
    
    public void mainMenu()
    {
        sceneID = 0; 
        if(NextScene.SceneAdditive == true)
        {
            StartCoroutine(LoadSceneAdditive());
        }
        else
        {
            StartCoroutine(LoadScene());
        }
    }
    public void playGame()
    {
        sceneID = 1;
        if (NextScene.SceneAdditive == true)
        {
            StartCoroutine(LoadSceneAdditive());
        }
        else
        {
            StartCoroutine(LoadScene());
        }
    }
    public void toFirstCutScene()
    {
        sceneID = 2;
        if (NextScene.SceneAdditive == true)
        {
            StartCoroutine(LoadSceneAdditive());
        }
        else
        {
            StartCoroutine(LoadScene());
        }
    }
    public void trainStation()
    {
        sceneID = 3;
        if (NextScene.SceneAdditive == true)
        {
            StartCoroutine(LoadSceneAdditive());
        }
        else
        {
            StartCoroutine(LoadScene());
        }
    }
    public void outsideWorld()
    {
        sceneID = 4;
        if (NextScene.SceneAdditive == true)
        {
            StartCoroutine(LoadSceneAdditive());
        }
        else
        {
            StartCoroutine(LoadScene());
        }
    }
    public void saloon()
    {
        sceneID = 5;
        if (NextScene.SceneAdditive == true)
        {
            StartCoroutine(LoadSceneAdditive());
        }
        else
        {
            StartCoroutine(LoadScene());
        }
    }
    public void bank()
    {
        sceneID = 6;
        if (NextScene.SceneAdditive == true)
        {
            StartCoroutine(LoadSceneAdditive());
        }
        else
        {
            StartCoroutine(LoadScene());
        }
    }
    public void combat()
    {
        sceneID = 7;
        if (NextScene.SceneAdditive == true)
        {
            StartCoroutine(LoadSceneAdditive());
        }
        else
        {
            StartCoroutine(LoadScene());
        }
    }
    public void death()
    {
        sceneID = 8;
        if (NextScene.SceneAdditive == true)
        {
            StartCoroutine(LoadSceneAdditive());
        }
        else
        {
            StartCoroutine(LoadScene());
        }
    }
    public void win()
    {
        sceneID = 9;
        if (NextScene.SceneAdditive == true)
        {
            StartCoroutine(LoadSceneAdditive());
        }
        else
        {
            StartCoroutine(LoadScene());
        }
    }
    public void quitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneID);
    }
    IEnumerator LoadSceneAdditive()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Additive);
    }
}