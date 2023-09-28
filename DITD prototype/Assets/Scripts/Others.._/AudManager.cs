using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudManager : MonoBehaviour
{
    public static AudManager Instance;
    public bool playBackgroundMusic;
    public Sound[] musicSounds, sfxSounds, walkSound, dialogueSound;
    public AudioSource musicSource, sfxSource, walkSource, dialogueSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if(playBackgroundMusic == true)
        {
            PlayMusic("TownBG");

        }
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Music not found!!!");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Soundeffect not found!!!");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayWalk(string name)
    {
        Sound s = Array.Find(walkSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Walk sound not found!!!");
        }
        else
        {
            walkSource.clip = s.clip;
            walkSource.Play();
        }
    }

    public void PlayDialogue(string name)
    {
        Sound s = Array.Find(dialogueSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Dialgoue sound not found!!!");
        }
        else
        {
            dialogueSource.clip = s.clip;
            dialogueSource.Play();
        }
    }
}
