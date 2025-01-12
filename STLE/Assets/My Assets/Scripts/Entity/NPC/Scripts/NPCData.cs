using GD.Items;
using GD.Types;
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
    [Tooltip("Each dialogue said by the player, cycled through via int")]
    [SerializeField]
    private Dictionary<int, string[]> dialogues
         = new Dictionary<int, string[]>();

    [FoldoutGroup("Dialogues", expanded: true)]
    [Tooltip("The index of the dialogue which will load first")]
    [SerializeField]
    private int currentDialogue;

    [FoldoutGroup("Dialogues", expanded: true)]
    [Tooltip("The character's 'Voice'. Plays on each letter of their dialogue.")]
    [SerializeField]
    private AudioClip dialogueBeep;

    #endregion Fields

    #region Properties

    public string Character { get => character; set => character = value; }
    public Dictionary<int, string[]> Dialogues { get => dialogues; set => dialogues = value; }
    public int CurrentDialogue { get => currentDialogue; set => currentDialogue = value; }
    public AudioClip DialogueBeep { get => dialogueBeep; set => dialogueBeep = value; }

    #endregion Properties
}
