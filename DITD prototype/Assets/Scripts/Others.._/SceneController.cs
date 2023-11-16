using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }
    public void toFirstCutScene()
    {
        SceneManager.LoadScene(2);
    }
    public void trainStation()
    {
        //load trainStation
        SceneManager.LoadScene(3);
    }
    public void outsideWorld()
    {
        SceneManager.LoadScene(4);
    }
    public void saloon()
    {
        SceneManager.LoadScene(5);
    }
    public void bank()
    {
        SceneManager.LoadScene(6);
    }
    public void combat()
    {
        SceneManager.LoadScene(7);
    }
    public void death()
    {
        SceneManager.LoadScene(8);
    }
    public void win()
    {
        SceneManager.LoadScene(9);
    }
    public void quitGame()
    {
        Application.Quit();
    }
}