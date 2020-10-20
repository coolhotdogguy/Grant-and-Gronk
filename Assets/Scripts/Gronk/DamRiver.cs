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

            if (!dryPlanet)
            {
                promptText.text = "Dam River? Need " + requiredTemporalCoagulate.ToString() + " Temporal Coagulate. Press E";
            }
            else if (dryPlanet)
            {
                promptText.text = "River has been dammed. Change back?";
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!changeBack)
            {
                if (input)
                {
                    if (dryPlanet)
                    {
                        FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(0);
                        FindObjectOfType<PlayerData>().AddToInventoy(requiredTemporalCoagulate);
                        collectTC += requiredTemporalCoagulate;
                        FindObjectOfType<PlayerData>().dryPlanet = false;
                        changeBack = true;
                        dryPlanet = false;
                    }
                    else if (!dryPlanet && collectTC >= requiredTemporalCoagulate && !setPlanetBoolOnExit) //change planet type
                    {
                        FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(2);
                        FindObjectOfType<PlayerData>().SubtractFromInventroy(requiredTemporalCoagulate, 2);
                        setPlanetBoolOnExit = true;
                        promptText.text = "River has been dammed";

                    }
                    else if (!dryPlanet && collectTC < requiredTemporalCoagulate)
                    {
                        promptText.text = "Not Enough Temporal Coagulate to Alter Time. Needed " + (requiredTemporalCoagulate - collectTC).ToString();
                    }
                }
            }
            else
            {
                promptText.text = "River is no longer dammed";
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
