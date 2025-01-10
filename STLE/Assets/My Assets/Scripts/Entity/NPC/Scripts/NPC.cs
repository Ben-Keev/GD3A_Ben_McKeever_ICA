using GD.Events;
using UnityEngine;
using GD.Items;
using Unity.VisualScripting;
using GD.Selection;

/// <summary>
/// Represents an item that can be consumed by a game object on the correct layer
/// </summary>
/// <see cref="ItemData"/>
/// <see cref="ItemGameEvent"/>
public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    [Tooltip("The NPC data that represents this NPC")]
    private NPCData npcData;

    [SerializeField]
    [Tooltip("The event raised when NPC is interacted with")]
    private NPCGameEvent onNPCEvent;

    public bool interactible;

    private void Awake()
    {
        interactible = true;

        // Reset dialogue
        npcData.CurrentDialogue = 0;
    }

    /// <summary>
    /// Called when the item is interacted with (Most likely right clicked on)
    /// </summary>
    /// <param name="interactor">Reference to interactor object</param>
    public void Interact(GameObject interactor)
    {
        if (interactible)
        {
            //raise the event to notify listeners
            onNPCEvent?.Raise(npcData);

            // Cycle to the next possible dialogue
            cycleDialogue();
        }
    }

    public void SetInteractible(bool interactible)
    {
        this.interactible = interactible;
    }

    private void cycleDialogue()
    {
        if (npcData.CurrentDialogue < npcData.Dialogues.Count-1)
            npcData.CurrentDialogue++;
    }

    public void OnHover()
    {
        if (interactible)
        {
            transform.GetChild(1).GetComponent<Renderer>().enabled = true;
        }
    }

    public void OnDehover()
    {
        transform.GetChild(1).GetComponent<Renderer>().enabled = false;
    }
}