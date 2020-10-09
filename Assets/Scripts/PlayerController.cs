using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float jumpVelocity = 5f;
    [SerializeField] float playerSpeed = 5f;

    [Header("Jump Tuning")]


    //Awake
    Rigidbody2D rb2d;
    Collider2D coll;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void OnMove(InputValue value)
    {
        value.Get<float>(); // This will get float from action, left == -1f, right == 1f
    }

    void OnJump()
    {
        // jump code here
    }

    private void FixedUpdate()
    {
        /*if (Input.GetButtonDown("Jump"))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
        }
        if (Input.GetButtonUp("Jump"))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f); //Slows jump velocity if jump button is released
        }
        if (Input.GetAxisRaw("Horizontal") > 0) //Right is pressed
        {
            rb2d.velocity = new Vector2(playerSpeed, rb2d.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") < 0) //Left is pressed
        {
            rb2d.velocity = new Vector2(-playerSpeed, rb2d.velocity.y);
        }*/
    }
}
