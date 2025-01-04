using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles inputs related to 'dialogue'
/// https://github.com/Kaylon-Riordan/2-Mad-2-Fast/blob/main/2-Mad-2-Fast/Assets/My%20Game%20Assets/Scripts/Player/Player%20Input/PlayerInputHandler.cs
/// </summary>
public class PlayerDialogueInputHandler : PlayerInputHandler
{
    private InputAction advanceDialogue;

    private void Awake()
    {
        playerActions = GetComponent<PlayerInput>();
        InputActionMap actionMap = playerActions.actions.FindActionMap("UI");
        advanceDialogue = actionMap.FindAction("Advance Dialogue");
    }

    void OnEnable()
    {
        advanceDialogue.performed += DialogueBoxManager.Instance.AdvanceDialogue;
    }

    private void OnDisable()
    {
        advanceDialogue.performed -= DialogueBoxManager.Instance.AdvanceDialogue;
    }
}
