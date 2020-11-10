using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public int thisPlanetType = 0;

    private void Start()
    {
        if (thisPlanetType == (int)FindObjectOfType<TilemapSwapper2>().currentPlanetType)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}