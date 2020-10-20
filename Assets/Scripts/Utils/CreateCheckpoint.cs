using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCheckpoint : MonoBehaviour
{
    GameObject checkpointToSpawn;
    private void FixedUpdate()
    {
        checkpointToSpawn = new GameObject("Checkpoint");
        checkpointToSpawn.tag = "Checkpoint";
        checkpointToSpawn.transform.position = this.transform.position;
    }
}
