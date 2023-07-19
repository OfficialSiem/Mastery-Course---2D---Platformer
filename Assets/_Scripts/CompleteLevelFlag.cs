using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevelFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Anything with a PlayerMovementController is a player, therefore
        var player = collision.GetComponent<PlayerMovementController>();
        if (player != null)
        {
            //If some player did pass the Flag then...
            GameManager.Instance.MoveToNextLevel();
        }

    }
}
