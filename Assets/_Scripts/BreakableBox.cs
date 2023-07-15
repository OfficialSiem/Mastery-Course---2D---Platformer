using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Whatever hit it was a player, and the player is hitting the box from bellow-ish (lower corner hits count)
        if (collision.WasHitByPlayer() &&
            collision.WasHitFromBottomSide())
        {
            Destroy(gameObject);
        }

    }

}
