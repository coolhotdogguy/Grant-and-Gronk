using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown_Controller : MonoBehaviour
{
    Rigidbody2D rb2d;

    float horizontal;
    float vertical;

    [SerializeField] float runSpeed = 20.0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally

            float moveLimiter = Mathf.Sqrt(runSpeed * runSpeed / 2) / runSpeed; // Calculate moveLimiter using the Pythagorean theorem

            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        rb2d.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        // Print Out Velocity: Debug.Log(Mathf.Sqrt(rb2d.velocity.x * rb2d.velocity.x + rb2d.velocity.y * rb2d.velocity.y));
    }
}
