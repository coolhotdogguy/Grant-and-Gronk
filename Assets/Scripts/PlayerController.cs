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
    [SerializeField] float fallMultiplier = 2.5f;
    float jumpForgivenessBuffer = 0.1f; //allows jump a little before player lands
    float jumpForgivenessBufferTimer;
    float coyoteTime = 0.2f; //extra time to jump once platform is left
    float coyoteTimeTimer;

    [Header("Ground")]
    [SerializeField] LayerMask ground;
    bool isGrounded;

    //Awake
    Rigidbody2D rb2d;
    Collider2D coll;

    float move;
    bool jumping;


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

        if (coyoteTimeTimer > 0f && jumpForgivenessBufferTimer > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
            jumpForgivenessBufferTimer = 0;
        }

        if (rb2d.velocity.y < 0) //if falling, fall even faster
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; //-1 to offset unity's gravity
        }

    }

    void OnMove(InputValue value)
    {
        move = value.Get<float>(); // This will get float from action, left == -1f, right == 1f;
    }

    void OnJump(InputValue value)
    {
        jumping = value.isPressed;

        /*if (coyoteTimeTimer > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
        }*/
    }

    void OnJumpReleased()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * jumpButtonReleaseDeceleration);
    }

    private void HandleJumpForgivenessBuffer()
    {
        if (jumping)
        {
            jumpForgivenessBufferTimer = jumpForgivenessBuffer;
        }
        else
        {
            jumpForgivenessBufferTimer -= Time.deltaTime;
        }
    }

    private void HandleCoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimeTimer = coyoteTime;
        }
        else
        {
            coyoteTimeTimer -= Time.deltaTime;
        }
    }

}
