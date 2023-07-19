using UnityEngine;

public static class Collision2DExtensions
{
    /// <summary>
    ///     Using "this" helps us extend the methods of collisions
    ///     We can reference these into their own file, and we can now call these methods for anything using an object type Collision2D
    ///     Hence why we call the class "Collisions2DExtensions"
    ///     
    ///     Sometimes we might not be able to edit the base class "Collisions2D" type is basic and locked in Unity, can't augment outside
    ///     of what Unity allows. But these extension methods help us tack on - for us - noticably repeatable use-cases we want to
    ///     consolidate (eg if we have multiple in-game-boxes with "WasHitByPlayer" methods for collisions, we can consolidate them into
    ///     one extention class all those in-game-boxes can use in their code. And we can then alter/fix said method in just one file instead
    ///     of going to each file that uses WasHitByPlayer. In-short, we cleaned up what can be used/needed to be debugged!
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    public static bool WasHitByPlayer(this Collision2D collision)
    {
        return collision.collider.GetComponent<PlayerMovementController>() != null;
    }

    // For collisions, assume Bottom and Left are positive, Top and Right are
    // negative as the normals are relative to the direction of what collided
    // (e.g. if something hit your head, the object was traveling downward, hence the normal.y would be pointing down!
    public static bool WasBottom(this Collision2D collision)
    {
        return (collision.contacts[0].normal.y > 0.5);
    }

    public static bool WasTop(this Collision2D collision)
    {
        return (collision.contacts[0].normal.y < -0.5);
    }

    public static bool WasSide(this Collision2D collision)
    {
        return (collision.contacts[0].normal.x < -0.5) ||
                    (collision.contacts[0].normal.x > 0.5);

    }
}