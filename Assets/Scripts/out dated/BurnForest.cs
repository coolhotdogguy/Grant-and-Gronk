/*using System.Collections;
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

            if (!FindObjectOfType<PlayerData>().icePlanetUnlocked)
            {
                promptText.text = "Fracture the ancient ice?  " + requiredTemporalCoagulate.ToString() + "  Temporal Coagulate to change. Press E";
            }
            else if (FindObjectOfType<PlayerData>().icePlanetUnlocked)
            {
                promptText.text = "Ancient ice has been fractured.";
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (input)
            {
                if (!FindObjectOfType<PlayerData>().icePlanetUnlocked && collectTC >= requiredTemporalCoagulate)
                {
                    FindObjectOfType<PlayerData>().icePlanetUnlocked = true;
                    FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(1);
                    FindObjectOfType<PlayerData>().planetSwitcherCounter = 1;
                    FindObjectOfType<Icons>().FadeInPlanetIcon(1);
                    FindObjectOfType<Icons>().FadeOutPlanetIcon(FindObjectOfType<PlayerData>().previousPlanet);
                    promptText.text = "Ancient ice has been fractured.";
                    FindObjectOfType<SFXPlayer>().PlayIceBreakSound();

                }
                else if (collectTC < requiredTemporalCoagulate)
                {
                    promptText.text = "Not Enough Temporal Coagulate to Alter Time. Still need  " + (requiredTemporalCoagulate - collectTC).ToString();
                }
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
}*/
