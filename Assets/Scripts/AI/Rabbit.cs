using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Rabbit : MonoBehaviour
{
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float jumpLength = 5f;
    [SerializeField] float rightLimit;
    [SerializeField] float leftLimit;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask slipperyGround;
    [SerializeField] float bounceVelocity = 15f;
    [SerializeField] bool facingRight;
    bool isGrounded;
    bool isSlipperyGrounded;

    Animator animator;
    Rigidbody2D rb2d;
    Collider2D collider2d;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        // Physics2D.IgnoreCollision(FindObjectOfType<PlayerController>().gameObject.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<BoxCollider2D>());
        // Not needed because its already disabled in Unity’s Collision Matrix
    }

    private void Update()
    {
        isGrounded = collider2d.IsTouchingLayers(ground);
        isSlipperyGrounded = collider2d.IsTouchingLayers(slipperyGround);

        if (animator.GetBool("Jumping"))
        {
            if (rb2d.velocity.y < 0.1f)
            {
                animator.SetBool("Falling", true);
                animator.SetBool("Jumping", false);
            }
        }
        if (animator.GetBool("Falling"))
        {
            if (isGrounded || isSlipperyGrounded)
            {
                animator.SetBool("Falling", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<BoxCollider2D>());
        }
    }

    private void Move()
    {
        if(facingRight)
        {

            if (transform.position.x < rightLimit)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector2(-1, 1);
                }

                if (isGrounded || isSlipperyGrounded)
                {
                    rb2d.velocity = new Vector2(jumpLength, jumpHeight);
                    animator.SetBool("Jumping", true);
                }
            }
            else
            {
                facingRight = false;
            }
        }
        else
        {
            if (transform.position.x > leftLimit)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector2(1, 1);
                }

                if (isGrounded || isSlipperyGrounded)
                {
                    rb2d.velocity = new Vector2(-jumpLength, jumpHeight);
                    animator.SetBool("Jumping", true);
                }
            }
            else
            {
                facingRight = true;
            }
        }
    }

    public float BounceVelocity()
    {
        return bounceVelocity;
    }

}
