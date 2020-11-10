using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquashBug : MonoBehaviour
{
    bool input;
    bool bugsEnabled = true;
    bool setBoolOnTriggerExit;
    int collectedTC;
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] int requiredTemporalCoagulate = 20;
    private void Start()
    {
        collectedTC = FindObjectOfType<PlayerData>().collectedTemporalCoagulateInt;
        bugsEnabled = FindObjectOfType<PlayerData>().enableBugs;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(bugsEnabled)
            {
                promptText.text = "Squash the ancestral bug?  " + requiredTemporalCoagulate.ToString() + "  Temporal Coagulate to change. Press E";
            }
            else if (!bugsEnabled)
            {
                promptText.text = "Bug's lineage has been squished, E to change back.";
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (input)
            {
                if (collectedTC < requiredTemporalCoagulate)
                {
                    promptText.text = "Not Enough Temporal Coagulate to Alter Time. Still need  " + (requiredTemporalCoagulate - collectedTC).ToString();
                }
                else if (collectedTC >= requiredTemporalCoagulate && bugsEnabled && !setBoolOnTriggerExit)
                {
                    promptText.text = "Bug's lineage has been squished, return to change back.";
                    bugsEnabled = false;
                    SquashBugs();
                    setBoolOnTriggerExit = true;
                    FindObjectOfType<SFXPlayer>().PlayBugSquashSound();

                }
                else if (collectedTC >= requiredTemporalCoagulate && !bugsEnabled && !setBoolOnTriggerExit)
                {
                    promptText.text = "Bug's ancestral line restored.";
                    bugsEnabled = true;
                    SquashBugs();
                    setBoolOnTriggerExit = true;
                }

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!bugsEnabled)
        {
            promptText.text = "Bug's lineage has been squished, return to change back.";
        }
        if (bugsEnabled)
        {
            promptText.text = "";
        }

        if (collision.tag == "Player")
        {
            setBoolOnTriggerExit = false;
        }
    }

    private void SquashBugs()
    {
        FindObjectOfType<PlayerData>().enableBugs = bugsEnabled;
    }

    void OnEInput(InputValue value)
    {
        input = value.isPressed;
    }
}
