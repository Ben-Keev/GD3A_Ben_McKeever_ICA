using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles inputs related to 'exploring' the map
/// https://github.com/Kaylon-Riordan/2-Mad-2-Fast/blob/main/2-Mad-2-Fast/Assets/My%20Game%20Assets/Scripts/Player/Player%20Input/PlayerInputHandler.cs
/// </summary>
public class PlayerExploreInputHandler : PlayerInputHandler
{
    private PlayerController controller;
    private InputActionMap actionMap;

    public InputAction interact;
    public InputAction move;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        playerActions = GetComponent<PlayerInput>();

        actionMap = playerActions.actions.FindActionMap("Main");
        move = actionMap.FindAction("Move");
        // Interact is checked in the response scripts.
        interact = actionMap.FindAction("Interact");
    }

    void OnEnable()
    {
        actionMap.Enable();
        move.performed += controller.Move;
    }

    private void OnDisable()
    {
        actionMap.Disable();
        move.performed -= controller.Move;
    }
}
