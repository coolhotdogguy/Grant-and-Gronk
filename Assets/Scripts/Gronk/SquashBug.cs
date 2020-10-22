using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashBug : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SquashBugs();
        }
    }

    private void SquashBugs()
    {
        FindObjectOfType<LevelObjects>().disableBugs = true;
    }


    //finish bug squash
}
