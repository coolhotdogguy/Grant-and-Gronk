using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] int thisPlanetType = 0;

    private void Start()
    {
        if(thisPlanetType == FindObjectOfType<TilemapSwapper2>().currentPlanetType)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
