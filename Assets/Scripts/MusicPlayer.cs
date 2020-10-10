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

    float fadeDuration;
    float fadeLeft;
    bool isFadeOut;
    bool isFadeIn;

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
        audioSource.Stop();
        audioSource.clip = music[scene.buildIndex];
        audioSource.Play();
    }

    private void Update()
    {
        fadeLeft -= Time.deltaTime;
        if (isFadeOut && fadeLeft >= 0f)
        {
            audioSource.volume = Mathf.Clamp(fadeLeft / fadeDuration, 0f, 1f);
        }
        else if(isFadeIn && fadeLeft >= 0f)
        {
            audioSource.volume = 1 - Mathf.Clamp(fadeLeft / fadeDuration, 0f, 1f);
        }

        if(audioSource.volume <= 0f + fadeVolumeThreshold && isFadeOut)
        {
            audioSource.volume = 0f;
            isFadeOut = false;
        }
        else if(audioSource.volume >= 1f - fadeVolumeThreshold && isFadeIn)
        {
            audioSource.volume = 1f;
            isFadeIn = false;
        }
    }
}
