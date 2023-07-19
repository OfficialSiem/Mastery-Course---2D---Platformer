using UnityEngine;

public class UiCoinImage : MonoBehaviour
{
    private Animator animator;

    //We want our GameManger to be instanciated first, so we're gonna caching/checking for a Pulse on Start
    void Start()
    {
        animator = GetComponent<Animator>();
        GameManager.Instance.OnCoinsChanged += Pulse;
    }

    private void Pulse(int coins)
    {
        animator.SetTrigger("Pulse");
    }

    /// <summary>
    ///     There's a bug in Unity, where if you reload a level...
    ///         "even though unity destroyed the game object, it didn't actually break those references. 
    ///         And that script happens to also be referencing the audio source, so maybe it keeps that reference around too. 
    ///         Then when the scene reloads there's some conflict with the two audio sources, since there's still a reference left 
    ///         over from before. You would expect a scene unload to clear all that out though.."
    ///    So you need to have an OnDestroy method that deletes the references when, well, everything is Disabled/Destroyed
    ///         See, https://forum.unity.com/threads/missingreferenceexception-when-scene-is-reloaded.533658/ for more details Thanks #Tyc1Up
    /// </summary>
    private void OnDestroy() { 
        GameManager.Instance.OnCoinsChanged -= Pulse;
    }
}
