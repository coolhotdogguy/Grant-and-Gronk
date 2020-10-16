using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DamRiver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    bool input;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.text = "Dam River? Press E";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (input)
            {
                Debug.Log("River be Dammed!");
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

    void OnTestingInputs(InputValue value)
    {
        input = value.isPressed;
    }

}
