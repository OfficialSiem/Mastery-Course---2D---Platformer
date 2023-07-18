using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    private new Collider2D collider;

    private new Rigidbody2D rigidbody2D;
    private Vector2 direction = Vector2.left;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + direction*speed*Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (ReachedEdge())
            SwitchedDirection();
    }
    private bool ReachedEdge()
    {
        //float x is equal to, if direction is -1 use collider.min; if not, use bounds max!
        // 0.1 is an offset to check a little bit past the left (negative) and right (positive)
        // sides of the collider
        float x = direction.x == -1 ?
            collider.bounds.min.x - 0.1f :
            collider.bounds.max.x + 0.1f ;

        float y = collider.bounds.min.y;

        Vector2 origin = new Vector2(x, y);
        Debug.DrawRay(origin, Vector2.down*0.1f, Color.yellow);

        var hit = Physics2D.Raycast(origin, Vector2.down, 0.1f);

        //if hit by a collider return true, otherwise nothing was hit, we got a null value, so return false!
        return hit.collider == null ? true : false; 
            
    }

    private void SwitchedDirection()
    {
        direction *= -1;
    }
}
