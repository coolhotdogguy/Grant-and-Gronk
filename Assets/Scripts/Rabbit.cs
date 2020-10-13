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
    [SerializeField] float bounceVelocity = 15f;
    bool facingRight;


    Animator anim;
    Rigidbody2D rb2d;
    Collider2D coll;



    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        //Physics.IgnoreLayerCollision(9, 10);
    }

    private void Update()
    {
        if (anim.GetBool("Jumping"))
        {
            if (rb2d.velocity.y < 0.1f)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
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

                if (coll.IsTouchingLayers(ground))
                {
                    rb2d.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
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

                if (coll.IsTouchingLayers(ground))
                {
                    rb2d.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
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
