using System.Collections;
using TMPro;
using UnityEngine;
using System.IO;
using GD;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using GD.Items;
using GD.Audio;
using GD.Types;
using UnityEditor;
using Sirenix.OdinInspector;

/// <summary>
/// Displays spoken dialogue to the screen
/// https://www.youtube.com/watch?v=8oTYabhj248
/// </summary>
public class DialogueBox : MonoBehaviour
{

    [FoldoutGroup("Text Components")]
    [Tooltip("Component displaying spoken dialogue")]
    [SerializeField]
    private TextMeshProUGUI dialogueTextComponent;

    [FoldoutGroup("Text Components")]
    [Tooltip("Component displaying character's name")]
    [SerializeField]
    private TextMeshProUGUI nameTextComponent; 

    [FoldoutGroup("Configuration")]
    [Tooltip("How long to wait between inserting letters")]
    [SerializeField]
    private float textWait; 

    [FoldoutGroup("Audio")]
    [Tooltip("Plays on advance of DialogueBox")]
    [SerializeField]
    private AudioClip advanceSound; 

    private int currentLine;
    private string[] lines;
    
    // Currently speaking NPC's dialogueBeep
    AudioClip currentDialogueBeep;

    private bool displayed;

    public bool Displayed { get => displayed; set => displayed = value; }

    private void Awake()
    {
        DisplayDialogue(false);
    }

    /// <summary>
    /// Open a dialogue box
    /// </summary>
    /// <param name="NpcData">NPC from whom dialogue and dialoguebeep will be read</param>
    public void StartDialogue(NPCData NpcData)
    {
        lines = NpcData.CurrentDialogue.Value;
        currentDialogueBeep = NpcData.DialogueBeep;

        nameTextComponent.text = NpcData.Character;

        currentLine = 0;

        DisplayDialogue(true);
        StartCoroutine(TypeLine());
    }

    /// <summary>
    /// Types letter by letter on an interval determined by textWait
    /// </summary>
    IEnumerator TypeLine()
    {
        foreach (char c in lines[currentLine].ToCharArray())
        {

            dialogueTextComponent.text += c;

            if (currentDialogueBeep != null)
            AudioManager.Instance.PlaySound(currentDialogueBeep, AudioMixerGroupName.Voiceover);
        
            yield return new WaitForSeconds(textWait);
        }
    }

    /// <summary>
    /// Skips animation and displays full text or goes to the next line of text depending state of animation.
    /// </summary>
    /// <param name="context">Players "AdvanceDialogue" action</param>
    public void AdvanceDialogue(InputAction.CallbackContext context)
    {
        if (dialogueTextComponent.text == lines[currentLine]) // All the text is displayed. Move onto the next line.
        {
            AudioManager.Instance.PlaySound(advanceSound, AudioMixerGroupName.UI);
            NextLine();
        }
        else // Skip the animation and display all the text
        {
            StopAllCoroutines();
            dialogueTextComponent.text = lines[currentLine];
        }
    }

    /// <summary>
    /// Start reading the next line of dialogue
    /// </summary>
    private void NextLine()
    {
        if (currentLine < lines.Length - 1)
        {
            currentLine++;
            dialogueTextComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        else // All the dialogue has been read. Close DIalogue.
        {
            DisplayDialogue(false);
            dialogueTextComponent.text = string.Empty; // Empty the dialogue text component
        }
    }

    /// <summary>
    /// Toggles whether dialogue components and its children are visible
    /// </summary>
    /// <param name="enabled">Desired state</param>
    private void DisplayDialogue(bool enabled)
    {
        // Record current state
        Displayed = enabled;

        // Toggle Dialogue Panel
        GetComponent<Image>().enabled = enabled;
        // Toggle Dialogue text
        dialogueTextComponent.enabled = enabled;

        // Toggle Name Panel
        transform.GetChild(1).GetComponent<Image>().enabled = enabled;
        // Toggle Name text
        nameTextComponent.enabled = enabled;
    }
}
