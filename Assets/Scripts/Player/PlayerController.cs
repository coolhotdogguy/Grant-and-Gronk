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
    [SerializeField] float playerSpeedIceMultiplier = 10f;
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
    public LayerMask slipperyGround;
    bool isGrounded;

    [Header("Physics Materials")]
    [SerializeField] PhysicsMaterial2D NoDrag;
    [SerializeField] PhysicsMaterial2D IcyDrag;


    //Awake / Start
    Rigidbody2D rb2d;
    Collider2D coll;
    Animator anim;

    float move;
    bool isJumpButtonPressed;
    float jump;
    bool bounce;
    bool isClimable;
    bool enemyRepel;
    bool icePlanet;

    enum AnimationState { idle, running, jumping, falling}
    AnimationState state = AnimationState.idle;

   
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        rb2d.gravityScale = playerGravity;
        icePlanet = FindObjectOfType<TilemapSwapper2>().icePlanet;

        if (FindObjectOfType<PlayerData>().gronkLevel)
        {
            icePlanet = false;
        }
        else
        {
            transform.position = FindObjectOfType<PlayerData>().playerPosition; //if Grant Level, more player to previous possition
        }
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

        HandleAnimationState();
        anim.SetInteger("Animation State", (int)state);
    }

    private void FixedUpdate()
    {
        if (!icePlanet)
        {
            if (coll.sharedMaterial != NoDrag) coll.sharedMaterial = NoDrag;
            if(!enemyRepel) rb2d.velocity = new Vector2(playerSpeed * move, rb2d.velocity.y); //Horizontal movement
        }
        else if (icePlanet && move != 0)
        {
            rb2d.velocity += Vector2.right * playerSpeed * move * Time.deltaTime * playerSpeedIceMultiplier;
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -playerSpeed, playerSpeed), rb2d.velocity.y);
        }

        if(icePlanet && move != 0)
        {
            coll.sharedMaterial = NoDrag;
        }
        else if(icePlanet && move == 0)
        {
            coll.sharedMaterial = IcyDrag;
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
            state = AnimationState.jumping;
        }
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



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Climable")
        {
            isClimable = false;
            rb2d.gravityScale = playerGravity;
        }
    }

    void HandleAnimationState()
    {
            if (state == AnimationState.jumping && !bounce)
            {
                if (rb2d.velocity.y < 0.2f)
                {
                    state = AnimationState.falling;
                }
            }
            else if (state == AnimationState.jumping && bounce)
            {
                if (rb2d.velocity.y < 0.2f)
                {
                    state = AnimationState.falling;
                    bounce = false;
                }
            }
            else if (state == AnimationState.falling)
            {
                if (coll.IsTouchingLayers(ground) || coll.IsTouchingLayers(slipperyGround))
                {
                    state = AnimationState.idle;
                }
                else if (bounce)
                {
                    state = AnimationState.jumping;
                }
            }
            else if (Mathf.Abs(rb2d.velocity.x) > 2f)
            {
                state = AnimationState.running;
            }
            else if ((coll.IsTouchingLayers(ground) || coll.IsTouchingLayers(slipperyGround)))
            {
                state = AnimationState.idle;
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


    private void HandleBugCollision(Collider2D collision)
    {
        if (transform.position.x > collision.transform.position.x) //player is right of bug
        {
            rb2d.velocity = new Vector2(collision.GetComponent<Bug>().BounceBackForce().x, collision.GetComponent<Bug>().BounceBackForce().y);
            if (!enemyRepel)
            {
                FindObjectOfType<PlayerData>().DamagePlayer();
            }
        }
        else //player is left of bug
        {
            rb2d.velocity = new Vector2(-collision.GetComponent<Bug>().BounceBackForce().x, collision.GetComponent<Bug>().BounceBackForce().y);
            if (!enemyRepel)
            {
                FindObjectOfType<PlayerData>().DamagePlayer();
            }
        }
        bounce = true;
        enemyRepel = true;
        StartCoroutine(FreezeHorizontalMovement());
    }

    IEnumerator FreezeHorizontalMovement()
    {
        yield return new WaitForSeconds(0.5f);
        yield return enemyRepel = false;

    }

}
