using GD.Events;
using UnityEngine;
using GD.Items;

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

    private void Awake()
    {
        // Reset dialogue
        npcData.CurrentDialogue = 0;
    }

    /// <summary>
    /// Called when the item is interacted with (Most likely right clicked on)
    /// </summary>
    /// <param name="interactor">Reference to interactor object</param>
    public void Interact(GameObject interactor)
    {
        //raise the event to notify listeners
        onNPCEvent?.Raise(npcData);

        // Cycle to the next possible dialogue
        cycleDialogue();
    }

    private void cycleDialogue()
    {
        if (npcData.CurrentDialogue < npcData.Dialogues.Count-1)
            npcData.CurrentDialogue++;
    }
}