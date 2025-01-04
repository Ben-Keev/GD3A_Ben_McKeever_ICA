using GD;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueBoxManager : Singleton<DialogueBoxManager>
{
    public DialogueBox db;

    public void LoadDialogue(string interaction)
    {
        // Read lines from file https://www.youtube.com/watch?v=sXjhwKPoEVA
        db.StartDialogue(File.ReadAllLines(Application.dataPath + "/Resources/Text/" + interaction + ".txt"));
    }

    public void AdvanceDialogue(InputAction.CallbackContext context)
    {
        db.AdvanceDialogue();
    }
}
