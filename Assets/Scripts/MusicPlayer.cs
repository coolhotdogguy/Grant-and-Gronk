using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] music;
    public AudioClip[] musicDistorted;
    public AudioClip[] ambient;

    float fadeDuration;
    float fadeLeft;
    bool isFade;

    void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void Awake()
    {
        audioSource.clip = music[0];
        audioSource.Play();
        DontDestroyOnLoad(this);
    }

    public void LinearFade(float duration)
    {
        fadeLeft = duration;
        fadeDuration = duration;
        isFade = true;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode scenemode)
    {
        audioSource.clip = music[scene.buildIndex];
        audioSource.Play();
    }

    private void Update()
    {
        if(isFade && fadeLeft >= 0f)
        {
            audioSource.volume = fadeLeft / fadeDuration;
        }
        else if(isFade && fadeLeft < 0f)
        {
            audioSource.volume = 0f;
        }
        if(audioSource.volume == 0f)
        {
            isFade = false;
            audioSource.Stop();
            audioSource.volume = 1f;
        }
        fadeLeft -= Time.deltaTime;
    }
}
