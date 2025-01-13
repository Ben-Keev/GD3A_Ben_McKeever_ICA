using GD.FSM;
using UnityEngine;

/// <summary>
/// Checks whether dialogue box is currently visible
/// </summary>

[CreateAssetMenu(menuName = "GD/FSM/Predicate/DialogueBoxDisplayed")]
public class PredicateDialogueBoxDisplayed : PredicateBase
{

    [SerializeField]
    private bool isDialogueShown;

    override public bool Evaluate(GameObject context = null)
    {
        // Storing the dialogue box as a variable gives outdated parameters when we call for displayed.
        // We must find it in the scene instead.
        return GameObject.FindGameObjectWithTag("DialogueBox").GetComponent<DialogueBox>().Displayed == isDialogueShown;
    }
}