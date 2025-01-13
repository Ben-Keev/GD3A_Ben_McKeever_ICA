using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All data related to NPCs
/// </summary>
[CreateAssetMenu(fileName = "NPCData", menuName = "GD/Data/NPC")]
public class NPCData : SerializedScriptableObject
{
    #region Fields

    [FoldoutGroup("Character", expanded: true)]
    [Tooltip("The character's name")]
    [SerializeField]
    private string character;

    [FoldoutGroup("Dialogues", expanded: true)]
    [Tooltip("Each set of dialogue said by the player")]
    [SerializeField]
    private LinkedList<string[]> dialogues
     = new LinkedList<string[]>();

    [FoldoutGroup("Dialogues", expanded: true)]
    [Tooltip("The dialogue that will be spoken on the next interaction.")]
    private LinkedListNode<string[]> currentDialogue;

    [FoldoutGroup("Dialogues", expanded: true)]
    [Tooltip("The character's 'Voice'. Plays on each letter of their dialogue.")]
    [SerializeField]
    private AudioClip dialogueBeep;

    #endregion Fields

    #region Properties

    public string Character { get => character; set => character = value; }

    public LinkedListNode<string[]> CurrentDialogue { get => currentDialogue; set => currentDialogue = value; }
    public AudioClip DialogueBeep { get => dialogueBeep; set => dialogueBeep = value; }
    public LinkedList<string[]> Dialogues { get => dialogues; set => dialogues = value; }

    #endregion Properties

    /// <summary>
    /// Remove all old dialogue stored in NPC Data and replace it with new dialogue.
    /// </summary>
    /// <param name="dialogue">The new dialogue</param>
    public void OverwriteDialogue(string[] dialogue)
    {
        dialogues.Clear();
        Dialogues.AddFirst(dialogue);
        currentDialogue = dialogues.First;
    }
}
