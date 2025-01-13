using GD.Events;
using UnityEngine;
using GD.Items;
using GD.Audio;
using GD.Types;
using Sirenix.OdinInspector;
using System.Xml.Linq;

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

    // NPCs have no variation between audio clips so keep the audio here.
    [FoldoutGroup("UI & Sound", expanded: true)]
    [SerializeField]
    [Tooltip("The audio clip that represents this item")]
    private AudioClip audioClip;

    [FoldoutGroup("UI & Sound")]
    [SerializeField]
    [Tooltip("The position of the audio source that plays the audio clip")]
    private Vector3 audioPosition;

    public bool interactible;

    public NPCData NpcData { get => npcData; set => npcData = value; }

    private void Awake()
    {
        interactible = true;

        // Reset dialogue

        if (NpcData.Dialogues.Count != 0)
            NpcData.CurrentDialogue = NpcData.Dialogues.First;

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
            onNPCEvent?.Raise(NpcData);

            if(audioClip != null)
            AudioManager.Instance.PlaySound(audioClip, AudioMixerGroupName.SFX);

            // https://stackoverflow.com/questions/7113347/assignment-in-an-if-statement
            // Assign within an if statement
            if (GetComponentInChildren<Animator>() is Animator NPCanim)
                NPCanim.SetTrigger("hurt");

            if(interactor.transform.GetComponentInChildren<Animator>() is Animator playerAnim)
                playerAnim.SetTrigger("attack");

            // Cycle to the next possible dialogue
            CycleDialogue();
        }
    }

    public void SetInteractible(bool interactible)
    {
        this.interactible = interactible;
    }

    private void CycleDialogue()
    {
        if (NpcData.CurrentDialogue != NpcData.Dialogues.Last)
            NpcData.CurrentDialogue = NpcData.CurrentDialogue.Next;
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