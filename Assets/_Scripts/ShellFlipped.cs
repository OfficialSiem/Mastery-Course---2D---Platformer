using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellFlipped : MonoBehaviour
{
    [SerializeField]
    private float shellSpeed = 5f;

    private new Collider2D collider;
    private new Rigidbody2D rigidbody2D;

    private Vector2 direction = Vector2.zero;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Keep the y velocity, but change the X velocity base on how fast we set the shell speed
        rigidbody2D.velocity = new Vector2(direction.x * shellSpeed, rigidbody2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.WasHitByPlayer())
        {
            var playerMovementController = collision.collider.GetComponent<PlayerMovementController>();
            if (direction.magnitude == 0)
            {
                LaunchShell(collision);
                playerMovementController.Bounce();
            }
            else
            {
                //If you hit the thing on top, stop the shell
                if (collision.WasTop())
                {
                    direction = Vector2.zero;
                    playerMovementController.Bounce();
                }
            }
        } 
    }

    private void LaunchShell(Collision2D collision)
    {
        float floatDirection = collision.contacts[0].normal.x > 0 ? 1f : -1f;
        direction = new Vector2(floatDirection, 0);
    }
}
