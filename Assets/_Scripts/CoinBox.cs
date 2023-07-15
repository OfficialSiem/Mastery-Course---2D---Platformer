using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour
{

    //What does the Coin box look like when enabled
    [SerializeField]
    private SpriteRenderer enabledSprite;

    //What does the Coin box look like when all coins have been collected
    [SerializeField]
    private SpriteRenderer disabledSprite;

    //Total coins the box has
    [SerializeField]
    private int totalCoins = 1;

    //How many remaining coins the box has left
    private int remainingCoins;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        remainingCoins = totalCoins;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //If there are coins, and whatever entered it was a player, and the player is hitting the box from bellow-ish (lower corner hits count)
        if (remainingCoins > 0 &&
            WasHitByPlayer(collision) &&
            WasHitFromBottomSide(collision))
        {

            GameManager.Instance.AddCoin();
            remainingCoins--;
            animator.SetTrigger("coinBoxWasHit");
            if (remainingCoins <= 0)
            {
                enabledSprite.enabled = false;
                disabledSprite.enabled = true;
            }
        }

    }

    private static bool WasHitByPlayer(Collision2D collision)
    {
        return collision.collider.GetComponent<PlayerMovementController>() != null;
    }

    private static bool WasHitFromBottomSide(Collision2D collision)
    {
        return (collision.contacts[0].normal.y > 0.5);
    }
}
