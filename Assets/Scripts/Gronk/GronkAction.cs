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
    Icons icons;

    bool input;
    bool setPlanetOnExit;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        icons = FindObjectOfType<Icons>();
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
                if (!(switchTo == PlanetType.Ice ? playerData.icePlanetUnlocked : playerData.dryPlanetUnlocked) && playerData.collectedTemporalCoagulateInt >= requiredTemporalCoagulate)
                {
                    playerData.icePlanetUnlocked = true;
                    FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(switchTo);
                    playerData.planetSwitcherCounter = (int)switchTo;
                    icons.FadeInPlanetIcon((int)switchTo);
                    icons.FadeOutPlanetIcon(playerData.previousPlanet);
                    promptText.text = done;
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