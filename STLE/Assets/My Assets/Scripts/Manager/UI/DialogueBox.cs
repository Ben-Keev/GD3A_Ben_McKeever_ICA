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

/// <summary>
/// 
/// https://www.youtube.com/watch?v=8oTYabhj248
/// </summary>
public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogueTextComponent; // Textcomponent where text will be displayed

    [SerializeField]
    private TextMeshProUGUI nameTextComponent; // Textcomponent where text will be displayed

    [SerializeField]
    private float textWait; // How long to wait between inserting a character.

    [SerializeField]
    private AudioClip advanceSound; // Plays on advance of dialoguebox

    private int index;
    private string[] lines; // Each box of dialogue.
    public bool displayed;

    AudioClip currentVoice;

    private void Awake()
    {
        DisplayDialogue(false);
    }

    public void StartDialogue(NPCData data)
    {
        lines = data.CurrentDialogue.Value;
        currentVoice = data.DialogueBeep;

        nameTextComponent.text = data.Character;

        index = 0;

        DisplayDialogue(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {

            dialogueTextComponent.text += c;

            if (currentVoice != null)
            AudioManager.Instance.PlaySound(currentVoice, AudioMixerGroupName.Voiceover);
        
            yield return new WaitForSeconds(textWait);
        }
    }

    public void AdvanceDialogue(InputAction.CallbackContext context)
    {
        if (dialogueTextComponent.text == lines[index])
        {
            AudioManager.Instance.PlaySound(advanceSound, AudioMixerGroupName.UI);
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueTextComponent.text = lines[index];
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueTextComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        else // All the dialogue has been read
        {
            DisplayDialogue(false);
            dialogueTextComponent.text = string.Empty; // Empty the dialogue
        }
    }

    private void DisplayDialogue(bool isDisplayed)
    {
        displayed = isDisplayed;
        GetComponent<Image>().enabled = isDisplayed;
        transform.GetChild(1).GetComponent<Image>().enabled = isDisplayed;
        dialogueTextComponent.enabled = isDisplayed;
        nameTextComponent.enabled = isDisplayed;
    }
}
