using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool Passed { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Anything with a PlayerMovementController is a player, therefore
        var player = collision.GetComponent<PlayerMovementController>();
        if(player != null )
        {
            //If some player did pass the checkpoint than, say a player "Passed" the checkpoint
            Passed = true;
        }
    }
    
}
