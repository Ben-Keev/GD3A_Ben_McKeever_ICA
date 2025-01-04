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

    public InputAction interact;
    public InputAction move;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        playerActions = GetComponent<PlayerInput>();

        InputActionMap actionMap = playerActions.actions.FindActionMap("Main");
        move = actionMap.FindAction("Move");
        interact = actionMap.FindAction("Interact");
        Debug.Log("Awake");
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
        move.performed += controller.Move;
        interact.performed += controller.Interact;
    }

    private void OnDisable()
    {
        move.performed -= controller.Move;
        interact.performed -= controller.Interact;
    }
}
