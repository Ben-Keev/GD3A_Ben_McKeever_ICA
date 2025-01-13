using GD.Events;
using UnityEngine;
using GD.Items;
using System;
using Sirenix.OdinInspector;

/// <summary>
/// Bin that can be interacted with to deposit items
/// </summary>
/// <see cref="BinData"/>
/// <see cref="BinGameEvent"/>
public class Bin : MonoBehaviour, IInteractable
{
    [FoldoutGroup("Data", expanded: true)]
    [SerializeField]
    [Tooltip("The Bin Data of this bin")]
    private BinData binContents;

    [FoldoutGroup("Data", expanded: true)]
    [SerializeField]
    [Tooltip("The event raised when Bin is interacted with")]
    private BinGameEvent onBinEvent;

    [FoldoutGroup("Runtime Info")]
    public bool interactable;

    public bool Interactable { get => interactable; set => interactable = value; }

    private void Awake()
    {
        // Interactible by default
        Interactable = true;

        // Reset inventory from previous playthrough
        binContents.BinContents.Clear();
    }

    /// <summary>
    /// Called when bin interacted with.
    /// </summary>
    /// <param name="interactor">Reference to interactor object</param>
    public void Interact(GameObject interactor)
    {
        if (Interactable)
        {
            // The player has a unique animation for interacting with bins and NPCs
            if (interactor.transform.GetComponentInChildren<Animator>() is Animator playerAnim)
                playerAnim.SetTrigger("attack");

            // Store the necessary data to raise an event
            Tuple<Transform, BinData> eventData = new Tuple<Transform, BinData>(transform, binContents);

            //raise the event to notify listeners
            onBinEvent?.Raise(eventData);
        }
    }

    /// <summary>
    /// Toggle whether interactaable
    /// </summary>
    /// <param name="interactable">Desired State</param>
    public void SetInteractable(bool interactable)
    {
        Interactable = interactable;
    }

    /// <summary>
    /// An outline is drawn around the bin
    /// </summary>
    public void OnHover()
    {
        if (Interactable)
            GetComponent<Outline>().enabled = true;
    }

    /// <summary>
    /// The outline is removed
    /// </summary>
    public void OnDehover()
    {
        GetComponent<Outline>().enabled = false;
    }
}