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

    [FoldoutGroup("character", expanded: true)]
    [Tooltip("The character's name")]
    [SerializeField]
    private string character;

    [FoldoutGroup("Dialogues", expanded: true)]
    [Tooltip("Each dialogue said by the player, cycled through via int")]
    [SerializeField]
    private Dictionary<int, string[]> dialogues
         = new Dictionary<int, string[]>();

    [FoldoutGroup("Current Dialogue", expanded: true)]
    [Tooltip("The index of the dialogue which will load first")]
    [SerializeField]
    private int currentDialogue;

    #endregion Fields

    #region Properties

    public string Character { get => character; set => character = value; }
    public Dictionary<int, string[]> Dialogues { get => dialogues; set => dialogues = value; }
    public int CurrentDialogue { get => currentDialogue; set => currentDialogue = value; }

    #endregion Properties
}
