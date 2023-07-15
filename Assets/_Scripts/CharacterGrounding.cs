using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterGrounding : MonoBehaviour
{
    [SerializeField]
    private Transform leftFoot = null;

    [SerializeField]
    private Transform rightFoot = null;

    //How far the raycast show look down
    [SerializeField]
    private float maxDepth = 0.3f;

    [SerializeField]
    private LayerMask layerMask = 0;

    /*So only 
     * the CharacterGrounding Script can SET the varaible
     * but every script can now read the field because its public*/

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        CheckFootForGrounding(leftFoot);

        if (IsGrounded == false)
            CheckFootForGrounding(rightFoot);
    }

    private void CheckFootForGrounding(Transform foot)
    {
        var raycastHit = Physics2D.Raycast(foot.position, Vector2.down, maxDepth, layerMask);
        Debug.DrawRay(foot.position, Vector3.down * maxDepth, Color.red);
        if (raycastHit.collider != null)
        {
            IsGrounded = true;
        }
        else
        {

            IsGrounded = false;
        }
    }

}
