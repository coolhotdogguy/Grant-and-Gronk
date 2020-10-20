﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    GameObject[] checkpoints;
    private void FixedUpdate()
    {

    }

    private void OnDrawGizmos()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        if (checkpoints.Length > 1)
        {
            for (int i = 0; i < checkpoints.Length - 1; i++)
            {
                Debug.DrawLine(checkpoints[i].transform.position, checkpoints[i + 1].transform.position, Color.red);
            }
        }
    }
}
