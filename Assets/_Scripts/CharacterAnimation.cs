using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    private IMove mover;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        mover = GetComponent<IMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Get the player's horizontal input as a float value (for controllers, +1 is right and -1 is left)
        float horizontal = Input.GetAxis("Horizontal");

        //Register the absolute value, so regarless of direction...
        //You can hold SHIFT+F12 to figure out all the references of a variable!
        float speed = mover.Speed;

        //the animator will animate running accordingly!
        animator.SetFloat("MovementSpeed", Mathf.Abs(speed));

        if(speed != 0)
            spriteRenderer.flipX = speed > 0;

    }
}
