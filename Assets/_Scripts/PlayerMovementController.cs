using System;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
/* Class Definitions
    Up here is the class definition, anything written after the ":" 
    references a "base class" (meaning PlayerMovementController has functions or methods
    defined by or just from MonoBehavior)
*/

//Anything that uses the PlayerMovement script MUST have a characterGrounding script (and a Rigidbody2D)
[RequireComponent (typeof(CharacterGrounding))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour, IMove
{

    //Changes player's velocity
    [SerializeField]
    private float moveSpeed = 3.5f;

    //Changes how far jump goes
    [SerializeField]
    private float jumpMagnitude = 400.0f;

    //How far we can wall jump
    [SerializeField]
    private float wallJumpForce = 250.0f;

    //How far the player bounces on  an object
    [SerializeField]
    private float bounceMagnitude = 200.0f;

    /*We use "new" to overide 
     * the existing definition of rigidBody2D; there are old methods of Rigidbody2D that
     exist, adn Unity can call those existing methods (good idea if Unity misdocuments 
    or failed to update other functions/variables properly, and we're calling older functionality for some reason
    now, you can just name it "private Rigidbody2D [insert-Variable-name-thats-not-"rigidbody2D"] and then you
    wouldn't need to use the word new; however, its important to learn when are where the keyword new may be used*/
    private new Rigidbody2D rigidbody2D;

    //This way we can use the script CharacterGrounding and access its vairables to determine whether or not the character is grounded or mid-air!
    private CharacterGrounding characterGrounding;


    public float Speed { get; private set; }



    /* When the gameObject spawns, or loads into a scene
     * The code here will fire off!
     */
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        characterGrounding = GetComponent<CharacterGrounding>();
    }

    private void Update()
    {
        //We should check for button pressed in Update, a single press can happen within
        //a frame (like how we see eSports players or Speedrunners talk about frame-perfect attacks,
        //jumps... so we should check for button presses every frame!
        if (characterGrounding.IsGrounded && (Input.GetButtonDown("Fire1")))
        {
            rigidbody2D.AddForce(Vector2.up * jumpMagnitude);

            //If the player is grounded on something that isn't directly bellow them (most likely a wall)
            if (characterGrounding.GroundedDirection != Vector2.down)
            {
                //then launch the character in the opposite direction of what's grounding them
                //((i.e. do a wall jump!) 
                rigidbody2D.AddForce(characterGrounding.GroundedDirection * -1f * wallJumpForce);
            }
        }
    }

    /*Prioritizes over Update,
    you want anything dealing with colliders, player movement, or physics to use FixedUpdate*/
    private void FixedUpdate()
    {
        //So you want Movement/Physics to happen and be calculated on Fixed Update
        //Infact the state of holding a direction button is longer than a single frame right?
        //so we only want to check for any directional movement every, so often
        float horizontal = Input.GetAxis("Horizontal");
        Speed = horizontal;

        Vector3 movement = new Vector3(horizontal, 0, 0);

        /* We use Time.deltaTime 
         * to change movement based on incremental values of frame-rates
         * This way, weather you use High-End systems or delpaidated hardware,
         * the movement progresses at the same, fraction of a frame-rate.
         * 
         * fixedDeltaTime is just the time between fixed updates, and technically
         * calling Time.deltaTime in fixedUpdate is just an akward implementation of Time.fixedDeltaTime
         * 
        */
        transform.position += movement * Time.fixedDeltaTime * moveSpeed;

    }

    internal void Bounce()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Vector2.up.y*bounceMagnitude);
    }
}
