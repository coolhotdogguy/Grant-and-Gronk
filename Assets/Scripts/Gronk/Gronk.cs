using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gronk : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] float playerSpeed = 9f;
    [SerializeField] float playerGravity = 2.5f;

    [Header("Jump Tuning")]
    [SerializeField] float jumpVelocity = 5f;
    [SerializeField] float jumpButtonReleaseDeceleration = 0.5f;
    float jumpForgivenessBuffer = 0.15f; //allows jump input a little before player lands
    float jumpForgivenessBufferTimer;
    bool handleJumpForgiveness;
    [SerializeField] float coyoteTime = 0.2f; //extra time to jump once platform is left
    float coyoteTimeTimer; //counts down when player leaves a ledge

    [Header("Ground")]
    public LayerMask ground;
    bool isGrounded;

    //Awake / Start
    Rigidbody2D rb2d;
    Collider2D coll;
    Animator anim;

    float move;
    bool isJumpButtonPressed;
    float jump;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        rb2d.gravityScale = playerGravity;
        FindObjectOfType<SFXPlayer>().SwitchAmbience(3);
        FindObjectOfType<MusicPlayer>().volume = 1f;
    }

    private void Update()
    {
        isGrounded = coll.IsTouchingLayers(ground);
        HandleCoyoteTime();

        HandleJumpForgivenessBuffer();

        if (Mathf.Abs(rb2d.velocity.x) > 2f)
        {
            //anim.SetBool("Running", true);
            //anim.SetBool("Idle", false);
            anim.speed = 1;
        }
        else if (coll.IsTouchingLayers(ground))
        {
            //anim.SetBool("Idle", true);
            //anim.SetBool("Running", false);
            anim.speed = 0;
        }

    }

    private void FixedUpdate()
    {   
        rb2d.velocity = new Vector2(playerSpeed * move, rb2d.velocity.y); //Horizontal movement

        if (!isJumpButtonPressed && rb2d.velocity.y > 0) //if jump is released while player is moving up
        {
            rb2d.velocity -= Vector2.up * rb2d.velocity.y * jumpButtonReleaseDeceleration; //slowdown jump velocity
        }
        if ((coyoteTimeTimer > 0f && jump == 1) || (jumpForgivenessBufferTimer > 0f && isGrounded && isJumpButtonPressed))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
            jumpForgivenessBufferTimer = 0f;
            coyoteTimeTimer = 0f;
            jump = 0;
        }
    }

    void OnMove(InputValue value)
    {
        move = value.Get<float>();

        if (move > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }


    void OnJump(InputValue value)
    {
        isJumpButtonPressed = value.isPressed;

        if (!isGrounded && value.isPressed && coyoteTimeTimer <= 0f)
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
        if (handleJumpForgiveness && jumpForgivenessBufferTimer <= 0f)
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
