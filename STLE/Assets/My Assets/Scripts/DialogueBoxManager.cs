using GD;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueBoxManager : Singleton<DialogueBoxManager>
{
    [SerializeField]
    private GameObject DialogueGameObject;

    private void Start()
    {
        LoadDialogue("student_a");
    }

    public void LoadDialogue(string interaction)
    {
        // Read lines from file https://www.youtube.com/watch?v=sXjhwKPoEVA
        DialogueGameObject.GetComponent<DialogueBox>().StartDialogue(File.ReadAllLines(Application.dataPath + "/Resources/Text/" + interaction + ".txt"));
    }

    public void AdvanceDialogue(InputAction.CallbackContext context)
    {
        DialogueGameObject.GetComponent<DialogueBox>().AdvanceDialogue();
    }
}
