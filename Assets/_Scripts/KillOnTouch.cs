using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillOnTouch : MonoBehaviour
{
    /// <summary>
    /// This is just checking if something has entered in its personal space.
    ///     Personal Space being, whatever hit the gameobject's collider (hence Collision) 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Var is just a short-cut, a non-specifier, that helps when you dont care to specify or dont know what type you need
        var playerMovmentController = collision.collider.GetComponent<PlayerMovementController>();
        if(playerMovmentController != null)
        {
            GameManager.Instance.KillPlayer();

        }
    }


}
