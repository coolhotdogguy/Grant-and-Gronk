using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip[] hurtSounds;
    [SerializeField] AudioClip pickupSound;
    [SerializeField] AudioClip bounceSound;
    [SerializeField] AudioClip normalAmbi;
    [SerializeField] AudioClip iceAmbi;
    [SerializeField] AudioClip dryAmbi;
    [SerializeField] AudioClip gronkAmbi;
    [SerializeField] AudioClip iceBreakSound;
    [SerializeField] AudioClip damSound;
    [SerializeField] AudioClip timeTravelSound;
    [SerializeField] AudioClip bugSquashSound;

    [Header("UI")]
    [SerializeField] AudioClip back;
    [SerializeField] AudioClip settings;
    [SerializeField] AudioClip exit;
    [SerializeField] AudioClip start;

    void Start()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        };

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHurtSound()
    {
        int i = Random.Range(0, 2);
        audioSource.PlayOneShot(hurtSounds[i]);
    }

    public void PlayPickupSound()
    {
        audioSource.PlayOneShot(pickupSound);
    }

    public void PlayBounceSound()
    {
        audioSource.PlayOneShot(bounceSound);
    }

    public void PlayIceBreakSound()
    {
        audioSource.PlayOneShot(iceBreakSound);
    }

    public void PlayDamSound()
    {
        audioSource.PlayOneShot(damSound);
    }

    public void PlayTimeTravelSound()
    {
        audioSource.PlayOneShot(timeTravelSound);
    }

    public void PlayBugSquashSound()
    {
        audioSource.PlayOneShot(bugSquashSound);
    }

    public void PlayBackSound()
    {
        audioSource.PlayOneShot(back);
    }

    public void PlayExitSound()
    {
        audioSource.PlayOneShot(exit);
    }

    public void PlayStartSound()
    {
        audioSource.PlayOneShot(start);
    }

    public void PlaySettingsSound()
    {
        audioSource.PlayOneShot(settings);
    }


    public void SwitchAmbience(int i)
    {
        if (i == 0)
        {
            audioSource.clip = normalAmbi;
        }
        if (i == 1)
        {
            audioSource.clip = iceAmbi;
        }
        if (i == 2)
        {
            audioSource.clip = dryAmbi;
        }
        if (i == 3)//gronk
        {
            audioSource.clip = null;
        }

        audioSource.Play();
    }
}
