using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GronkAction : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] int requiredTemporalCoagulate;
    [SerializeField] PlanetType switchTo;

    [Header("Texts")]
    [SerializeField] string question;
    [SerializeField] string done;

    PlayerData playerData;

    bool input;
    bool setPlanetOnExit;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!FindObjectOfType<PlayerData>().icePlanetUnlocked)
            {
                promptText.text = question + "  " + requiredTemporalCoagulate.ToString() + "  Temporal Coagulate to change. Press E";
            }
            else if (FindObjectOfType<PlayerData>().icePlanetUnlocked)
            {
                promptText.text = done;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (input)
            {
                if (!FindObjectOfType<PlayerData>().icePlanetUnlocked && playerData.collectedTemporalCoagulateInt >= requiredTemporalCoagulate)
                {
                    FindObjectOfType<PlayerData>().icePlanetUnlocked = true;
                    FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(1);
                    FindObjectOfType<PlayerData>().planetSwitcherCounter = 1;
                    FindObjectOfType<Icons>().FadeInPlanetIcon(1);
                    FindObjectOfType<Icons>().FadeOutPlanetIcon(FindObjectOfType<PlayerData>().previousPlanet);
                    promptText.text = "Ancient ice has been fractured.";
                    FindObjectOfType<SFXPlayer>().PlayIceBreakSound();

                }
                else if (playerData.collectedTemporalCoagulateInt < requiredTemporalCoagulate)
                {
                    promptText.text = "Not Enough Temporal Coagulate to Alter Time. Still need  " + (requiredTemporalCoagulate - playerData.collectedTemporalCoagulateInt).ToString();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            promptText.text = "";
            if (setPlanetOnExit)
            {
                icePlanet = true;
                setPlanetOnExit = false;
            }
            changeBack = false;
        }
    }

    void OnEInput(InputValue value)
    {
        input = value.isPressed;
    }
}

public enum PlanetType
{
    Forest,
    Ice,
    Desert
}