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
            }
            if(jumperTemporalCoagulate)
            {
                FindObjectOfType<PlayerController>().jumpVelocity = 22f;
                FindObjectOfType<Icons>().EnableBunnyIcon();
            }
            this.gameObject.SetActive(false);
        }
    }

}
