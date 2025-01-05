using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles inputs related to 'dialogue'
/// https://github.com/Kaylon-Riordan/2-Mad-2-Fast/blob/main/2-Mad-2-Fast/Assets/My%20Game%20Assets/Scripts/Player/Player%20Input/PlayerInputHandler.cs
/// </summary>
public class PlayerDialogueInputHandler : PlayerInputHandler
{
    [SerializeField]
    private DialogueBox dialogueBox;
    private InputAction advanceDialogue;
    private InputActionMap actionMap;

    private void Awake()
    {
        playerActions = GetComponent<PlayerInput>();
        actionMap = playerActions.actions.FindActionMap("UI");
        advanceDialogue = actionMap.FindAction("Advance Dialogue");
    }

    void OnEnable()
    {
        actionMap.Enable();
        advanceDialogue.performed += dialogueBox.AdvanceDialogue;
    }

    private void OnDisable()
    {
        actionMap.Disable();
        advanceDialogue.performed -= dialogueBox.AdvanceDialogue;
    }
}
