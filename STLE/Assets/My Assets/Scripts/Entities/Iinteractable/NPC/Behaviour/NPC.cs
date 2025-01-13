using GD.Events;
using UnityEngine;
using GD.Items;
using GD.Audio;
using GD.Types;
using Sirenix.OdinInspector;

/// <summary>
/// Represents an NPC that can be spoken to
/// </summary>
public class NPC : MonoBehaviour, IInteractable
{

    [FoldoutGroup("Data", expanded: true)]
    [SerializeField]
    [Tooltip("NPC Data containing this NPC's name and dialogue")]
    private NPCData npcData;

    [FoldoutGroup("Data", expanded: true)]
    [SerializeField]
    [Tooltip("The event raised when NPC is interacted with")]
    private NPCGameEvent onNPCEvent;

    [FoldoutGroup("UI & Sound", expanded: true)]
    [SerializeField]
    [Tooltip("Audio that plays on interaction")]
    private AudioClip onInteractionAudio;

    [FoldoutGroup("Runtime Info")]
    public bool interactable;

    public NPCData NpcData { get => npcData; set => npcData = value; }
    public bool Interactable { get => interactable; set => interactable = value; }

    private void Awake()
    {
        interactable = true; // interactable by default

        // Reset dialogue if dialogue exists.
        if (NpcData.Dialogues.Count != 0)
            NpcData.CurrentDialogue = NpcData.Dialogues.First;
    }

    /// <summary>
    /// Called as a response when NPC is interacted with
    /// </summary>
    /// <param name="interactor">The entity that initiated the interaction</param>
    public void Interact(GameObject interactor)
    {
        // Can't interact if not interactable
        if (Interactable)
        {
            //raise the event to notify listeners
            onNPCEvent?.Raise(NpcData);

            if(onInteractionAudio != null)
            AudioManager.Instance.PlaySound(onInteractionAudio, AudioMixerGroupName.SFX);

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

    /// <summary>
    /// Enables or disables interactability
    /// </summary>
    /// <param name="interactable">Enable or disable paramater</param>
    public void SetInteractable(bool interactable)
    {
        Interactable = interactable;
    }

    /// <summary>
    /// Goes to the next set of dialogue boxes.
    /// </summary>
    private void CycleDialogue()
    {
        if (NpcData.CurrentDialogue != NpcData.Dialogues.Last)
            NpcData.CurrentDialogue = NpcData.CurrentDialogue.Next;
    }

    /// <summary>
    /// Reveals the "Chat" icon over the NPC's head.
    /// </summary>
    public void OnHover()
    {
        if (Interactable)
            transform.GetChild(1).GetComponent<Renderer>().enabled = true;
    }


    /// <summary>
    /// Hides the "Chat" icon over the NPC's head.
    /// </summary>
    public void OnDehover()
    {
        transform.GetChild(1).GetComponent<Renderer>().enabled = false;
    }
}