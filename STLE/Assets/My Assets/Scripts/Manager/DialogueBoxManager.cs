using GD;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueBoxManager : Singleton<DialogueBoxManager>
{
    public GameObject dialogueGameObject;

    public void LoadDialogue(string interaction)
    {
        // Read lines from file https://www.youtube.com/watch?v=sXjhwKPoEVA
        dialogueGameObject.GetComponent<DialogueBox>().StartDialogue(File.ReadAllLines(Application.dataPath + "/Resources/Text/" + interaction + ".txt"));
    }

    public void AdvanceDialogue(InputAction.CallbackContext context)
    {
        dialogueGameObject.GetComponent<DialogueBox>().AdvanceDialogue();
    }
}
