using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalCoagulate : MonoBehaviour
{
    [SerializeField] bool jumperTemporalCoagulate;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!jumperTemporalCoagulate)
            {
                FindObjectOfType<PlayerData>().AddToInventoy(1);
                FindObjectOfType<SFXPlayer>().PlayPickupSound();
            }
            if(jumperTemporalCoagulate)
            {
                FindObjectOfType<PlayerController>().jumpVelocity = 22f;
                FindObjectOfType<PlayerController>().invincible = true;
                FindObjectOfType<Icons>().EnableBunnyIcon();
                FindObjectOfType<PlayerData>().HandleHealthUI();
            }
            this.gameObject.SetActive(false);
        }
    }

}
