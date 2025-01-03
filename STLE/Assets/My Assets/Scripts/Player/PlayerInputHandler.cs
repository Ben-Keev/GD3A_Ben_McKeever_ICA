using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Binds a player's inputs as according to their desired control scheme
/// https://github.com/Kaylon-Riordan/2-Mad-2-Fast/blob/main/2-Mad-2-Fast/Assets/My%20Game%20Assets/Scripts/Player/Player%20Input/PlayerInputHandler.cs
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerActions;
    private InputAction move;
    private InputAction advanceDialogue;

    // TODO remove
    public DialogueBox dialogueBox;

    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        playerActions = GetComponent<PlayerInput>();

        initaliseGameplayInput();
        initialiseUiInput();
    }

    private void initaliseGameplayInput()
    {
        InputActionMap actionMap = playerActions.actions.FindActionMap("Main");

        // Find Actions
        move = actionMap.FindAction("Move");
    }

    private void initialiseUiInput()
    {
        InputActionMap actionMap = playerActions.actions.FindActionMap("UI");

        // Find Actions
        advanceDialogue = actionMap.FindAction("Advance Dialogue");
    }

    private void OnEnable()
    {
        move.performed += controller.Move;
        advanceDialogue.performed += dialogueBox.AdvanceDialogue;
    }

    private void OnDisable()
    {
        playerActions.actions.FindActionMap("Main").Disable();
        playerActions.actions.FindActionMap("UI").Disable();
    }
}
