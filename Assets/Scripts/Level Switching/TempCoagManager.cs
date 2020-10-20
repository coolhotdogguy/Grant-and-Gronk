using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCoagManager : MonoBehaviour
{

    [SerializeField] GameObject[] tempCoags;

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

    public void EnableAllTemporalCoagulate()
    {
        for (int i = 0; i < tempCoags.Length; i++)
        {
            tempCoags[i].SetActive(true);
        }
    }
}
