using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalCoagulate : MonoBehaviour
{
    [SerializeField] bool jumperTemporalCoagulate;
    PlayerData playerData;
    PlayerController playerController;
    Icons icons;
    SFXPlayer sFXPlayer;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        playerController = FindObjectOfType<PlayerController>();
        icons = FindObjectOfType<Icons>();
        sFXPlayer = FindObjectOfType<SFXPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!jumperTemporalCoagulate)
            {
                playerData.AddToInventoy(1);
                sFXPlayer.PlayPickupSound();
            }
            if(jumperTemporalCoagulate)
            {
                playerController.jumpVelocity = 22f;
                playerData.invincible = true;
                icons.EnableBunnyIcon();
                playerData.HandleHealthUI();
            }
            this.gameObject.SetActive(false);
        }
    }

}
