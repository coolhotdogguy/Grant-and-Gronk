using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BurnForest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI promptText;
    bool input;
    [SerializeField] int requiredTemporalCoagulate = 5;
    int collectTC;
    [HideInInspector] public bool icePlanet;
    bool setPlanetBoolOnExit;
    bool changeBack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collectTC = FindObjectOfType<PlayerData>().collectedTemporalCoagulateInt;
            icePlanet = FindObjectOfType<PlayerData>().icePlanet;

            if (!icePlanet)
            {
                promptText.text = "Burn Forest? Need " + requiredTemporalCoagulate.ToString() + " Temporal Coagulate to change. Press E";
            }
            else if (icePlanet)
            {
                promptText.text = "Forest has been burned. Change Back?";
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (input && !FindObjectOfType<PlayerData>().icePlanetUnlocked && collectTC >= requiredTemporalCoagulate)
            {
                FindObjectOfType<PlayerData>().icePlanetUnlocked = true;
                FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(1);
                FindObjectOfType<PlayerData>().planetSwitcherCounter = 1;
            }
            else if (!changeBack)
            {
                if (input)
                {
                    if (icePlanet)
                    {
                        FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(0);
                        //FindObjectOfType<PlayerData>().AddToInventoy(requiredTemporalCoagulate);
                        //collectTC += requiredTemporalCoagulate;
                        FindObjectOfType<PlayerData>().icePlanet = false;
                        changeBack = true;
                        icePlanet = false;
                    }
                    else if (!icePlanet && collectTC >= requiredTemporalCoagulate && !setPlanetBoolOnExit)
                    {
                        FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(1);
                        //FindObjectOfType<PlayerData>().SubtractFromInventroy(requiredTemporalCoagulate, 1);
                        setPlanetBoolOnExit = true;
                        promptText.text = "The forest is burnt";
                        FindObjectOfType<PlayerData>().icePlanetUnlocked = true;
                    }
                    else if (!icePlanet && collectTC < requiredTemporalCoagulate)
                    {
                        promptText.text = "Not Enough Temporal Coagulate to Alter Time. Needed " + (requiredTemporalCoagulate - collectTC).ToString();
                    }
                }
            }
            else
            {
                promptText.text = "The forest is no longer burnt";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            promptText.text = "";
            if (setPlanetBoolOnExit)
            {
                icePlanet = true;
                setPlanetBoolOnExit = false;
            }
            changeBack = false;
        }
    }

    void OnEInput(InputValue value)
    {
        input = value.isPressed;
    }
}
