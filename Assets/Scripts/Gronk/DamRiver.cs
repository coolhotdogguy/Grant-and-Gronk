using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DamRiver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI promptText;
    bool input;
    [SerializeField] int requiredTemporalCoagulate = 5;
    int collectTC;
    [HideInInspector] public bool dryPlanet;
    bool setPlanetBoolOnExit;
    bool changeBack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collectTC = FindObjectOfType<PlayerData>().collectedTemporalCoagulateInt;
            dryPlanet = FindObjectOfType<PlayerData>().dryPlanet;

            if (!FindObjectOfType<PlayerData>().dryPlanetUnlocked)
            {
                promptText.text = "Dam the primordial river? Stil need " + requiredTemporalCoagulate.ToString() + " Temporal Coagulate. Press E";
            }
            else if (FindObjectOfType<PlayerData>().dryPlanetUnlocked)
            {
                promptText.text = "River has been dammed.";
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (input)
            {
                if (!FindObjectOfType<PlayerData>().dryPlanetUnlocked && collectTC >= requiredTemporalCoagulate)
                {
                    FindObjectOfType<PlayerData>().dryPlanetUnlocked = true;
                    FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(2);
                    FindObjectOfType<PlayerData>().planetSwitcherCounter = 2;
                    FindObjectOfType<Icons>().FadeInPlanetIcon(2);
                    FindObjectOfType<Icons>().FadeOutPlanetIcon(FindObjectOfType<PlayerData>().previousPlanet);
                    promptText.text = "River has been dammed";
                    FindObjectOfType<SFXPlayer>().PlayDamSound();
                }
                else if (collectTC < requiredTemporalCoagulate)
                {
                    promptText.text = "Not Enough Temporal Coagulate to Alter Time. Still need  " + (requiredTemporalCoagulate - collectTC).ToString();
                }
            }


            /*
            if (input)
                {
                    if (dryPlanet)
                    {
                        FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(0);
                        //FindObjectOfType<PlayerData>().AddToInventoy(requiredTemporalCoagulate);
                        //collectTC += requiredTemporalCoagulate;
                        FindObjectOfType<PlayerData>().dryPlanet = false;
                        dryPlanet = false;
                    }
                    else if (!dryPlanet)
                    {
                        if (collectTC >= requiredTemporalCoagulate && !setPlanetBoolOnExit) //change planet type
                        {
                            FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(2);
                            //FindObjectOfType<PlayerData>().SubtractFromInventroy(requiredTemporalCoagulate, 2);
                            setPlanetBoolOnExit = true;
                            promptText.text = "River has been dammed";
                            FindObjectOfType<PlayerData>().dryPlanetUnlocked = true;
                        }
                        if (collectTC < requiredTemporalCoagulate)
                        {
                            promptText.text = "Not Enough Temporal Coagulate to Alter Time. Needed " + (requiredTemporalCoagulate - collectTC).ToString();
                        }
                    }
                }
            */
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            promptText.text = "";
            if (setPlanetBoolOnExit)
            {
                dryPlanet = true;
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
