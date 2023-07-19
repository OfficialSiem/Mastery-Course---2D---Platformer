using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private GameObject spawnOnStompPrefab;

    private new Collider2D collider;

    private new Rigidbody2D rigidbody2D;
    private Vector2 direction = Vector2.left;
    private SpriteRenderer sprite;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.WasHitByPlayer())
        { 
            if (collision.WasTop())
            {
                HandleWalkerStomped();
            }
            else
            {
                GameManager.Instance.KillPlayer();
            }
        }
    }

    private void HandleWalkerStomped()
    {
        if( spawnOnStompPrefab != null )
        {
            Instantiate(spawnOnStompPrefab, transform.position, transform.rotation);
        }
        
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + direction*speed*Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        if (ReachedEdge() || HitNotPlayer())
            SwitchedDirection();
    }

    private bool HitNotPlayer()
    {
        float x = GetForwardX();
        float y = transform.position.y;

        Vector2 origin = new Vector2(x, y);
        Debug.DrawRay(origin, direction * 0.1f, Color.yellow);
        var hit = Physics2D.Raycast(origin, direction, 0.1f);

        //if hit by a collider return nothing, return false!
        if(hit.collider == null) return false;

        //if you hit something that was a trigger, return false
        if(hit.collider.isTrigger) return false;

        //if what you hit wasnt a player, return false!
        if(hit.collider.GetComponent<PlayerMovementController>() != null)
            return false;

        return true;

    }

    private bool ReachedEdge()
    {
        //float x is equal to, if direction is -1 use collider.min; if not, use bounds max!
        // 0.1 is an offset to check a little bit past the left (negative) and right (positive)
        // sides of the collider
        float x = GetForwardX();

        float y = collider.bounds.min.y;

        Vector2 origin = new Vector2(x, y);
        Debug.DrawRay(origin, Vector2.down * 0.1f, Color.yellow);

        var hit = Physics2D.Raycast(origin, Vector2.down, 0.1f);

        //if hit by a collider return true, otherwise nothing was hit, we got a null value, so return false!
        return hit.collider == null ? true : false;

    }

    private float GetForwardX()
    {
        return direction.x == -1 ?
                    collider.bounds.min.x - 0.1f :
                    collider.bounds.max.x + 0.1f;
    }

    private void SwitchedDirection()
    {
        direction *= -1;
        sprite.flipX = !sprite.flipX;
    }
}
