using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float playerSpeed = 5f;

    [Header("Jump Tuning")]
    [SerializeField] float jumpVelocity = 5f;
    [SerializeField] float jumpButtonReleaseDeceleration = 0.5f;
    float jumpForgivenessBuffer = 0.15f; //allows jump input a little before player lands
    float jumpForgivenessBufferTimer;
    bool handleJumpForgiveness;
    [SerializeField] float coyoteTime = 0.2f; //extra time to jump once platform is left
    float coyoteTimeTimer; //counts down when player leaves a ledge
    // [SerializeField] float fallMultiplier = 2.5f; was used for faster fall after peak of jump

    [Header("Ground")]
    public LayerMask ground;
    bool isGrounded;

    //Awake
    Rigidbody2D rb2d;
    Collider2D coll;

    float move;
    bool isJumpButtonPressed;
    float jump;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        isGrounded = coll.IsTouchingLayers(ground); 

        HandleCoyoteTime();

        HandleJumpForgivenessBuffer();
    }


    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(playerSpeed * move, rb2d.velocity.y); //Horizontal movemnet

        if (!isJumpButtonPressed && rb2d.velocity.y > 0) //if jump is released while player is moving up
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * jumpButtonReleaseDeceleration); //slowdown jump velocity
        }
        if ((coyoteTimeTimer > 0f && jump == 1) || (jumpForgivenessBufferTimer > 0f && isGrounded))
        {
            Debug.Log(0);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
            jumpForgivenessBufferTimer = 0f;
            coyoteTimeTimer = 0f;
            jump = 0;
        }

        /*if (rb2d.velocity.y < 0) //if falling, fall even faster
        {
            rb2d.velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime; //-1 to offset unity's gravity
        }*/

    }

    void OnMove(InputValue value)
    {
        move = value.Get<float>(); // This will get float from action, left == -1f, right == 1f;
    }

    void OnJump(InputValue value)
    {
        isJumpButtonPressed = value.isPressed;

        if (!isGrounded && value.isPressed) 
        {
            handleJumpForgiveness = true;
            jumpForgivenessBufferTimer = jumpForgivenessBuffer;
            return;
        }

        jump = value.Get<float>();
    }

    private void HandleJumpForgivenessBuffer()
    {
        if (handleJumpForgiveness)
        {
            jumpForgivenessBufferTimer -= Time.deltaTime;
        }
        if(handleJumpForgiveness && jumpForgivenessBufferTimer <= 0f)
        {
            handleJumpForgiveness = false;
            jumpForgivenessBufferTimer = 0f;
        }
    }

    private void HandleCoyoteTime()
    {
        if (isGrounded && rb2d.velocity.y == 0)
        {
            coyoteTimeTimer = coyoteTime;
        }
        else
        {
            coyoteTimeTimer -= Time.deltaTime;
        }
    }

}
