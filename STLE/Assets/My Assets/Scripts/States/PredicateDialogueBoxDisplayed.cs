using GD.FSM;
using GD.Types;
using UnityEngine;

/// <summary>
/// Checks whether dialogue box is currently visible
/// </summary>

[CreateAssetMenu(menuName = "GD/FSM/Predicate/DialogueBoxDisplayed")]
public class PredicateDialogueBoxDisplayed : PredicateBase
{
    [SerializeField]
    private bool isDialogueShown;

    override public bool Evaluate() {
        return DialogueBoxManager.Instance.dialogueGameObject.GetComponent<DialogueBox>().displayed == isDialogueShown;
    }
}
