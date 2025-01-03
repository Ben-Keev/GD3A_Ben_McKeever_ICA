using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 
/// https://www.youtube.com/watch?v=8oTYabhj248
/// </summary>
public class DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Textcomponent where text will be displayed
    public string[] lines; // Each box of dialogue.
    public float textWait; // How long to wait between inserting a character.

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty; // The dialogue box starts empty
        StartDialogue(); // The dialogue begins
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textWait);
        }
    }

    public void AdvanceDialogue(InputAction.CallbackContext context)
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    public void NextLine()
    {

        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        else
        {
            gameObject.SetActive(false);
        }
    }
}
