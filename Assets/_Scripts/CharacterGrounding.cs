using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterGrounding : MonoBehaviour
{
    [SerializeField]
    private Transform[] positions = null;



    //How far the raycast show look down
    [SerializeField]
    private float maxDepth = 0.3f;

    [SerializeField]
    private LayerMask layerMask = 0;


    private Transform groundedObject;

    //Adding a question mark just means that the variable could be null 
    //(which in most cases Vector3 can not be inittalized as null)
    private Vector3? groundedObjectLastPosition;


    /*So only 
     * the CharacterGrounding Script can SET the varaible
     * but every script can now read the field because its public*/
    public bool IsGrounded { get; private set; }

    private void Update()
    {
        foreach (var position in positions)
        {
            CheckFootForGrounding(position);
            if (IsGrounded)
                break;
        }

        StickToMovingObjects();
    }

    private void StickToMovingObjects()
    {
        //Check if there's an object the player is standing on
        if(groundedObject != null)
        {
            //If the position of the grounded object exists, and its not the same as the player
            if(groundedObjectLastPosition.HasValue && 
                groundedObjectLastPosition.Value != transform.position)
            {
                Vector3 delta = groundedObject.position - groundedObjectLastPosition.Value;
                transform.position += delta;
            }
            groundedObjectLastPosition = groundedObject.position;
        }
        //if the player's not on an object, set the last grounded object last's position to null
        else
        {
            groundedObjectLastPosition = null;
        }
    }

    private void CheckFootForGrounding(Transform foot)
    {
        var raycastHit = Physics2D.Raycast(foot.position, Vector2.down, maxDepth, layerMask);
        Debug.DrawRay(foot.position, Vector3.down * maxDepth, Color.red);
        if (raycastHit.collider != null)
        {
            //If we hit something that's not the same as the object we are currently on!
            if(groundedObject != raycastHit.collider.transform)
            {
               //make the new grounded object;s position
               // the latest position our foot can stay on
                groundedObjectLastPosition = raycastHit.collider.transform.position;
            }
            //regardless of newness, whatever our ray hitts becomes the newest grounded object
            //the player can interact with
            groundedObject = raycastHit.collider.transform;
            IsGrounded = true;

        }
        else
        {
            groundedObject = null;
            IsGrounded = false;
        }
    }

}
