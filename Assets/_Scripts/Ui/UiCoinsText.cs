using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiCoinsText : MonoBehaviour
{
    private TextMeshProUGUI tmproText;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        tmproText = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnCoinsChanged += HandleOnCoinsChanged;
        GameManager.Instance.OnCoinsChanged += Shake;
    }

    private void HandleOnCoinsChanged(int coins)
    {
        tmproText.text = coins.ToString();
    }

    private void Shake(int coins)
    {
        animator.SetTrigger("Shake");
    }


    /// <summary>
    ///     There's a bug in Unity, where if you reload a level...
    ///         "even though unity destroyed the game object, it didn't actually break those references. 
    ///         And that script happens to also be referencing the audio source, so maybe it keeps that reference around too. 
    ///         Then when the scene reloads there's some conflict with the two audio sources, since there's still a reference left 
    ///         over from before. You would expect a scene unload to clear all that out though.."
    ///    So you need to have an OnDestroy method that deletes the references when, well, everything is Disabled
    ///         See, https://forum.unity.com/threads/missingreferenceexception-when-scene-is-reloaded.533658/ for more details Thanks #Tyc1Up
    /// </summary>
    private void OnDisable()
    {
        GameManager.Instance.OnCoinsChanged -= HandleOnCoinsChanged;
        GameManager.Instance.OnCoinsChanged -= Shake;
    }

}
