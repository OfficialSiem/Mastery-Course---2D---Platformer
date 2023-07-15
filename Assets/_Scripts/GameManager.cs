using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Creates a literal, and prograble instance of the GameManager we can call from other scripts
    public static GameManager Instance { get; private set; }

    //Counts how many lives we have
    public int Lives { get; private set; }

    //A C# action that something caused the lives to change!
    public event Action<int> OnLivesChanged;

    //A C# action for when the coin count has changed!
    public event Action<int> OnCoinsChanged;

    //counts the number of coins
    //private characters should be lowercased, or sometimes you can use _coins to denote private variable using "_"
    private int coins;

    //This awake method will ensure only one instance of the GameManager is up, and because any other gameObject with GameManager will die
    //  The pattern of only keeping one total instance up and running is called a "Singleton" pattern!
    private void Awake()
    {
        //If the GameManger exists, DESTROY the GameObject on first load
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        //Otherwise, set the first instance of the GameManger to this, then set our lives counter, finally
        //never destroy this first GameObject
        else { 
            Instance = this;
            DontDestroyOnLoad(gameObject);

            RestartGame();
        }

    }


    //Litterally everything that happens when the player dies
    internal void KillPlayer()
    {
        Lives--;

        //Lets check if something caused the OnLivesChanged event to happened,
        if(OnLivesChanged != null)
        {
            //if so, lets start the event!
            OnLivesChanged(Lives);
        }

        if(Lives <= 0)
        {
            RestartGame();
            
        }
        else
        {
            SendPlayerToCheckpoint();
        }
        
    }

    private void SendPlayerToCheckpoint()
    {
        //Find the checkpoint manager..
        var checkpointManger = FindObjectOfType<CheckpointManager>();

        //Figure out what was the last checkpoint the player passed..
        var checkpoint = checkpointManger.GetLastCheckpointThatWasPassed();
        
        //then grab the player character..
        var player = FindAnyObjectByType<PlayerMovementController>();

        //and move them to where the last checkpoint was!
        player.transform.position = checkpoint.transform.position;
    }

    internal void AddCoin()
    {
        coins++;
        if(OnCoinsChanged != null)
        {
            OnCoinsChanged(coins);
        }
    }

    private void RestartGame()
    {
        Lives = 3;
        coins = 0;


        if (OnCoinsChanged != null)
        {
            OnCoinsChanged(coins);
        }

        SceneManager.LoadScene(0);
    }


}
