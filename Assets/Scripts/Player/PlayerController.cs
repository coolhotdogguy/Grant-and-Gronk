using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float playerGravity = 2.5f;

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
    public LayerMask slipperyGround;
    bool isGrounded;

    //Temporal Coagulate
    int temporalCoagulateInInventory;
    [SerializeField] TextMeshProUGUI temporalText;

    //Awake
    Rigidbody2D rb2d;
    Collider2D coll;
    PlayerHealth healthScript;

    float move;
    bool isJumpButtonPressed;
    float jump;
    bool bounce;
    bool isClimable;
    bool enemyRepel;
    [HideInInspector] public bool icePlanet;
   
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        healthScript = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        rb2d.gravityScale = playerGravity;
    }
    private void Update()
    {
        isGrounded = coll.IsTouchingLayers(ground) || coll.IsTouchingLayers(slipperyGround);

        if(isGrounded == true)
        {
            bounce = false;
        }

        HandleCoyoteTime();

        HandleJumpForgivenessBuffer();

    }


    private void FixedUpdate()
    {
        if (!icePlanet)
        {
            if (!enemyRepel)
            {
                rb2d.velocity = new Vector2(playerSpeed * move, rb2d.velocity.y); //Horizontal movement
            }
        }
        else if (coll.IsTouchingLayers(slipperyGround))
        {
            rb2d.velocity += Vector2.right * playerSpeed * move * Time.deltaTime;
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -playerSpeed, playerSpeed), rb2d.velocity.y);
        }

        if(!isGrounded && bounce == true) //avoids jump delceration when rabbit and bug bouncing
        { 
            return; 
        }

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

        /*if (rb2d.velocity.y < 0) //if falling, fall even faster
        {
            rb2d.velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime; //-1 to offset unity's gravity
        }*/
    }

    void OnMove(InputValue value)
    {
        move = value.Get<float>(); // This will get float from action, left == -1f, right == 1f;

        if (move > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }


    void OnJump(InputValue value)
    {
        isJumpButtonPressed = value.isPressed;

        if (isClimable)
        {
            jump = value.Get<float>();
            Climb();
        }

        if (!isGrounded && value.isPressed && coyoteTimeTimer <= 0f) 
        {
            handleJumpForgiveness = true;
            jumpForgivenessBufferTimer = jumpForgivenessBuffer;
            return;
        }

        jump = value.Get<float>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Rabbit":
                if (rb2d.velocity.y < 0.1f)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, collision.gameObject.GetComponent<Rabbit>().BounceVelocity());
                    bounce = true;
                }
                break;
            case "Climable":
                isClimable = true;
                break;
            case "Bug":
                HandleBugCollision(collision);
                break;
            default:
                break;
        }
    }

    private void HandleBugCollision(Collider2D collision)
    {
        if (transform.position.x > collision.transform.position.x) //player is right of bug
        {
            rb2d.velocity = new Vector2(collision.GetComponent<Bug>().BounceBackForce().x, collision.GetComponent<Bug>().BounceBackForce().y);
            healthScript.playerHealth--;
            healthScript.DamagePlayer();
        }
        else //player is left of bug
        {
            rb2d.velocity = new Vector2(-collision.GetComponent<Bug>().BounceBackForce().x, collision.GetComponent<Bug>().BounceBackForce().y);
            healthScript.playerHealth--;
            healthScript.DamagePlayer();
        }
        bounce = true;
        enemyRepel = true;
        StartCoroutine(FreezeHorizontalMovement());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Climable")
        {
            isClimable = false;
            rb2d.gravityScale = playerGravity;
        }
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

    private void Climb()
    {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jump * playerSpeed);
            rb2d.gravityScale = 0f;
    }

    IEnumerator FreezeHorizontalMovement()
    {
        yield return new WaitForSeconds(0.5f);
        yield return enemyRepel = false;
        
    }

    public void AddTemporalCoagulate()
    {
        temporalCoagulateInInventory++;
        temporalText.text = temporalCoagulateInInventory.ToString();
    }
}
