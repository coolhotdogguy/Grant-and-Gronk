using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class LevelObjectsEnabler : MonoBehaviour
{
    [SerializeField] GameObject[] forestObjects;
    [SerializeField] GameObject[] iceObjects;
    [SerializeField] GameObject[] dryObjects;

    [SerializeField] GameObject groundTileMap;
    [SerializeField] PlayerController player;

    int counter;


    void OnQInput(InputValue value)
    {
        if (value.isPressed)
        {
            counter++;
            if (counter > 2) { counter = 0; }

            if (counter == 0)//forest
            {
                for (int i = 0; i < forestObjects.Length; i++)
                {
                    forestObjects[i].SetActive(true);
                }
                for (int i = 0; i < iceObjects.Length; i++)
                {
                    dryObjects[i].SetActive(false);
                }
            }

            if (counter == 1)//ice
            {
                player.icePlanet = true;
                groundTileMap.layer = 11;

                for (int i = 0; i < iceObjects.Length; i++)
                {
                    iceObjects[i].SetActive(true);
                }
                for (int i = 0; i < forestObjects.Length; i++)
                {
                    forestObjects[i].SetActive(false);
                }
            }

            if (counter == 2)//dry
            {
                player.icePlanet = false;
                groundTileMap.layer = 8;

                for (int i = 0; i < dryObjects.Length; i++)
                {
                    dryObjects[i].SetActive(true);
                }
                for (int i = 0; i < iceObjects.Length; i++)
                {
                    iceObjects[i].SetActive(false);
                }
            }
        }
    }
}
