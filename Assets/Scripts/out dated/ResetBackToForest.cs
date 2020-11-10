/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class ResetBackToForest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    bool input;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.text = "Reset back to forest? Press E";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (input)
            {
                FindObjectOfType<DamRiver>().dryPlanet = false;
                FindObjectOfType<BurnForest>().icePlanet = false;
                FindObjectOfType<TilemapSwapper2>().SetPlanetTypeInt(0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.text = "";
        }
    }

    void OnEInput(InputValue value)
    {
        input = value.isPressed;
    }
}
*/