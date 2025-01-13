using GD.Events;
using UnityEngine;
using GD.Items;
using Unity.VisualScripting;
using GD.Selection;
using System;

/// <summary>
/// Represents an item that can be consumed by a game object on the correct layer
/// </summary>
/// <see cref="ItemData"/>
/// <see cref="ItemGameEvent"/>
public class Bin : MonoBehaviour, IInteractable
{
    [SerializeField]
    [Tooltip("The NPC data that represents this NPC")]
    private BinData binContents;

    [SerializeField]
    [Tooltip("The event raised when NPC is interacted with")]
    private BinGameEvent onBinEvent;

    public bool interactible;

    private void Awake()
    {
        interactible = true;

        // Reset inventory from previous playthrough
        binContents.BinContents.Clear();
    }

    /// <summary>
    /// Called when the item is interacted with (Most likely right clicked on)
    /// </summary>
    /// <param name="interactor">Reference to interactor object</param>
    public void Interact(GameObject interactor)
    {
        if (interactible)
        {
            Tuple<Transform, BinData> eventData = new Tuple<Transform, BinData>(transform, binContents);

            if (interactor.transform.GetComponentInChildren<Animator>() is Animator playerAnim)
                playerAnim.SetTrigger("attack");

            //raise the event to notify listeners
            onBinEvent?.Raise(eventData);
        }
    }

    public void SetInteractable(bool interactible)
    {
        this.interactible = interactible;
    }

    public void OnHover()
    {
        if (interactible)
            GetComponent<Outline>().enabled = true;
    }

    public void OnDehover()
    {
        GetComponent<Outline>().enabled = false;
    }
}