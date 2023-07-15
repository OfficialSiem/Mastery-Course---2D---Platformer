using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiLivesScript : MonoBehaviour
{
    private TextMeshProUGUI tmproText;
    private void Awake()
    {
        tmproText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnLivesChanged += HandleOnLiveChanged;
        tmproText.text = GameManager.Instance.Lives.ToString();

    }

    private void HandleOnLiveChanged(int livesRemaining)
    {
        tmproText.text = livesRemaining.ToString();
    }

    /// <summary>
    ///     There's a bug in Unity, where if you reload a level...
    ///         "even though unity destroyed the game object, it didn't actually break those references. 
    ///         And that script happens to also be referencing the audio source, so maybe it keeps that reference around too. 
    ///         Then when the scene reloads there's some conflict with the two audio sources, since there's still a reference left 
    ///         over from before. You would expect a scene unload to clear all that out though.."
    ///    So you need to have an OnDisable method that deletes the references when, well, everything is Disabled
    ///         See, https://forum.unity.com/threads/missingreferenceexception-when-scene-is-reloaded.533658/ for more details Thanks #Tyc1Up
    /// </summary>
    private void OnDisable()
    {
        GameManager.Instance.OnLivesChanged -= HandleOnLiveChanged;
    }
}
