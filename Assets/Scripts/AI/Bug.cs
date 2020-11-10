using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Bug : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float yTopLimit;
    [SerializeField] float yBottomLimit;
    [SerializeField] float bounceBackHorizontal = 30f;
    [SerializeField] float bounceBackVertical = 30f;

    Vector2 bounceBackForce;
    Vector2 topLimit;
    Vector2 bottomLimit;
    bool flyingSwitch;

    Rigidbody2D rb2d;
    Collider2D coll;

    private void Start()
    {
        topLimit = new Vector2(transform.position.x, yTopLimit);
        bottomLimit = new Vector2(transform.position.x, yBottomLimit);
        bounceBackForce = new Vector2(bounceBackHorizontal, bounceBackVertical);

    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 targetPosition = FlyingSwitch(); //handles knoing when to up/down
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            flyingSwitch = !flyingSwitch;
        }
    }

    private Vector2 FlyingSwitch()
    {
        if (flyingSwitch)
        {
            return topLimit;
        }
        else
        {
            return bottomLimit;
        }
    }

    public Vector2 BounceBackForce()
    {
        return bounceBackForce;
    }
}
