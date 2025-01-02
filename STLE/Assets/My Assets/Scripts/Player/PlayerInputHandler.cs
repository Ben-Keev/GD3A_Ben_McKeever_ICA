using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Binds a player's inputs as according to their desired control scheme
/// https://github.com/Kaylon-Riordan/2-Mad-2-Fast/blob/main/2-Mad-2-Fast/Assets/My%20Game%20Assets/Scripts/Player/Player%20Input/PlayerInputHandler.cs
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private InputActionMap playerActions;
    private InputAction move;

    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();

        playerActions = GetComponent<PlayerInput>().actions.FindActionMap("Main");

        // Find Actions
        move = playerActions.FindAction("Move");
    }

    private void OnEnable()
    {
        Debug.Log(controller);
        Debug.Log(move);
        move.performed += controller.Move;
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

}
