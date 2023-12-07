using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public Animator transitionAnim;
    public int sceneID;

    public void searchScenes(string searched)
    {
        if (searched == "Saloon")
            saloon();
        if (searched == "Bank Interrior")
            bank();
        if (searched == "Barn-stable")
            Debug.Log("you didn't set up barn yet");
        if (searched == "Combat")
            combat();
        if (searched == "cutSceneFirst")
            toFirstCutScene();
        if (searched == "LoseScene")
            lose();
        if (searched == "MainMenu")
            mainMenu();
        if (searched == "SampleScene")
            outsideWorld();
        if (searched == "Train Station")
            trainStation();
        if (searched == "WinScene")
            win();

    }
    
    public void mainMenu()
    {
        sceneID = 0; 
        StartCoroutine(LoadScene());
        
    }
    public void toFirstCutScene()
    {
        sceneID = 1;
        StartCoroutine(LoadScene());
        
    }
    public void trainStation()
    {
        sceneID = 2;
        StartCoroutine(LoadScene());
        
    }
    public void outsideWorld()
    {
        sceneID = 3;
        StartCoroutine(LoadScene());
        
    }
    public void saloon()
    {
        sceneID = 4;
        StartCoroutine(LoadScene());
        
    }
    public void bank()
    {
        sceneID = 5;
        StartCoroutine(LoadScene());
        
    }
    public void combat()
    {
        sceneID = 6;
        StartCoroutine(LoadScene());
        
    }
    public void lose()
    {
        sceneID = 7;
        StartCoroutine(LoadScene());
        
    }
    public void win()
    {
        sceneID = 8;
        StartCoroutine(LoadScene());
        
    }
    public void instructions()
    {
        sceneID = 9;
        StartCoroutine(LoadScene());
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

}