using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCoagDontDestroy : MonoBehaviour
{
    bool myBool;

    void Start()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (myBool)
        {

        }
    }
}
