using System;
using UnityEngine;

public class CoinAudio : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource> ();
    }
    /// <summary>
    ///   An Anonymous Lambda Function.. we can take the return type (or multiple return types if OnCoinsChanged returned 
    ///     multiple types (coins, images, etc.)
    ///     After telling the coins what to do, we then can tell this Start() method what to do as a result, in this case, its "audioSource.Play() or play the audio source"
    ///     If we wanted to do more than one thing, we can create a whole ass function '=> { if(thereIsAnAudioSource){audioSource.Play()}else{DoNothing()} ... andThenSome() ...}' end of Lambda
    ///     In a way were encapsulating/abstracting a method WITHIN the Start Method; and the world may never know!
    ///     Use the annonymous lambda version  when the object attached isn't geting destroyed (just helps with Unity de/re-registrater object references) -- cant re-register if the thing was Anon to being with
    ///         private void Start()
    ///         {
    ///              GameManager.Instance.OnCoinsChanged += (coins) => audioSource.Play();
    ///         }
/// </summary>

private void Start()
    {
        GameManager.Instance.OnCoinsChanged += PlayCoinAudio;
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
    private void OnDestroy()
    {
        GameManager.Instance.OnCoinsChanged -= PlayCoinAudio;
    }

    private void PlayCoinAudio(int coins)
    {
        audioSource.Play();
    }
}
