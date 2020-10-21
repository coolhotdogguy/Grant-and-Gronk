using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public float fadeVolumeThreshold;

    public AudioClip[] music;
    public AudioClip[] musicDistorted;
    public AudioClip[] ambient;

    public float volume = 1f;

    float fadeDuration;
    float fadeLeft;
    bool isFadeOut;
    bool isFadeIn;

    int lastScene = 0;

    void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void Awake()
    {
        audioSource.clip = music[0];
        audioSource.Play();
    }

    public void LinearFadeOut(float duration)
    {
        fadeLeft = duration;
        fadeDuration = duration;
        isFadeOut = true;
    }

    public void LinearFadeIn(float duration)
    {
        fadeLeft = duration;
        fadeDuration = duration;
        isFadeIn = true;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode scenemode)
    {
        if (lastScene != scene.buildIndex)
        {
            lastScene = scene.buildIndex;
            audioSource.Stop();
            //audioSource.clip = music[scene.buildIndex];
            audioSource.Play();
        }
    }

    private void Update()
    {
        if(!isFadeIn && !isFadeOut)
        {
            audioSource.volume = volume;
        }

        fadeLeft -= Time.deltaTime;
        if (isFadeOut && fadeLeft >= 0f)
        {
            audioSource.volume = Mathf.Lerp(0f, volume, Mathf.Clamp(fadeLeft / fadeDuration, 0f, 1f));
        }
        else if(isFadeIn && fadeLeft >= 0f)
        {
            audioSource.volume = Mathf.Lerp(0f, volume, 1 - Mathf.Clamp(fadeLeft / fadeDuration, 0f, 1f));
        }

        if(audioSource.volume <= 0f + fadeVolumeThreshold && isFadeOut)
        {
            audioSource.volume = 0f;
            isFadeOut = false;
        }
        else if(audioSource.volume >= volume - fadeVolumeThreshold && isFadeIn)
        {
            audioSource.volume = volume;
            isFadeIn = false;
        }
    }
}
