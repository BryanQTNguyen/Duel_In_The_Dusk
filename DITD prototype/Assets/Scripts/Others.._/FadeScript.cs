using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup;

    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;
    [SerializeField] public bool doneFadingIn = false;
    public bool doneFadingOut = true;

    void Start()
    {
        doneFadingOut = true;
    }
    public void ShowDialogue()
    {
        fadeIn = true;
        doneFadingIn = false;        
    }
    void Update()
    {
        if (fadeIn)
        {
            if (myUIGroup.alpha < 1)
            {
                myUIGroup.alpha += Time.deltaTime;
                if (myUIGroup.alpha >= 1)
                {
                    fadeIn = false;
                    doneFadingIn = true;
                }
            }
        }
        if (fadeOut)
        {
            if (myUIGroup.alpha >= 0)
            {
                myUIGroup.alpha -= Time.deltaTime;
                if (myUIGroup.alpha <= 0)
                {
                    fadeOut = false;
                    doneFadingOut = true;
                }
            }
        }
    }
    public void HideDialogue()
    {
        myUIGroup.alpha = 0;
    }

    public void HideDialogueFade()
    {
        fadeOut = true;
        doneFadingOut = false;
    }
}
