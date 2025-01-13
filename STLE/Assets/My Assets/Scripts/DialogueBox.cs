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
    private TextMeshProUGUI textComponent; // Textcomponent where text will be displayed

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
        lines = data.Dialogues[data.CurrentDialogue];
        currentVoice = data.DialogueBeep;

        index = 0;

        DisplayDialogue(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {

            textComponent.text += c;

            if (currentVoice != null)
            AudioManager.Instance.PlaySound(currentVoice, AudioMixerGroupName.Voiceover);
        
            yield return new WaitForSeconds(textWait);
        }
    }

    public void AdvanceDialogue(InputAction.CallbackContext context)
    {
        if (textComponent.text == lines[index])
        {
            AudioManager.Instance.PlaySound(advanceSound, AudioMixerGroupName.UI);
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        else // All the dialogue has been read
        {
            DisplayDialogue(false);
            textComponent.text = string.Empty; // Empty the dialogue
        }
    }

    private void DisplayDialogue(bool isDisplayed)
    {
        displayed = isDisplayed;
        GetComponent<Image>().enabled = isDisplayed;
        textComponent.GetComponent<TMP_Text>().enabled = isDisplayed;
        //Debug.Log("Visibility: " + isDisplayed);
    }
}
