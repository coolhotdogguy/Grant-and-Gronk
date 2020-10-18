using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjects : MonoBehaviour
{
    [SerializeField] GameObject[] forestObjects;
    [SerializeField] GameObject[] iceObjects;
    [SerializeField] GameObject[] dryObjects;

    [SerializeField] GameObject groundTileMap;

    int counter;

    private void Start()
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
    public void SetForestPlanet()
    {
        groundTileMap.layer = 8;
        for (int i = 0; i < forestObjects.Length; i++)
        {
            dryObjects[i].SetActive(false);
            forestObjects[i].SetActive(true);
            iceObjects[i].SetActive(false);
        }
    }
    public void SetIcePlanet()
    {
        groundTileMap.layer = 11;
        for (int i = 0; i < iceObjects.Length; i++)
        {
            iceObjects[i].SetActive(true);
            dryObjects[i].SetActive(false);
            forestObjects[i].SetActive(false);
        }
    }
    public void SetDryPlanet()
    {
        groundTileMap.layer = 8;
        for (int i = 0; i < dryObjects.Length; i++)
        {
            dryObjects[i].SetActive(true);
            forestObjects[i].SetActive(false);
            iceObjects[i].SetActive(false);
        }
    }
}
