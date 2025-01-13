using GD.Items;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles inputs related to 'exploring' the map
/// https://github.com/Kaylon-Riordan/2-Mad-2-Fast/blob/main/2-Mad-2-Fast/Assets/My%20Game%20Assets/Scripts/Player/Player%20Input/PlayerInputHandler.cs
/// </summary>
public class PlayerExploreInputHandler : MonoBehaviour
{
    private PlayerController controller;
    private InventoryManager inventoryManager;
    private InputActionMap actionMap;

    // Player controller inputs
    private InputAction interact;
    private InputAction move;

    // Inventory manager inputs
    private InputAction cycleUp;
    private InputAction cycleDown;

    // These inputs are checked in selection manager's raycast response scripts
    public InputAction Interact { get => interact; set => interact = value; }
    public InputAction Move { get => move; set => move = value; }

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();

        // Get the control scheme.
        PlayerInput playerActions = GetComponent<PlayerInput>();
        actionMap = playerActions.actions.FindActionMap("Main");

        // Player controller inputs
        Move = actionMap.FindAction("Move");
        Interact = actionMap.FindAction("Interact");

        // Inventory manager related inputs
        cycleUp = actionMap.FindAction("CycleUp");
        cycleDown = actionMap.FindAction("CycleDown");
    }

    void OnEnable()
    {
        actionMap.Enable();
        Move.performed += controller.StartMoving;
        Move.canceled += controller.StopMoving;
        cycleUp.performed += inventoryManager.CycleInventoryUp;
        cycleDown.performed += inventoryManager.CycleInventoryDown;
    }

    private void OnDisable()
    {
        actionMap.Disable();

        //Hald the character's movement.
        controller.StopMoving();

        Move.performed -= controller.StartMoving;
        cycleUp.performed -= inventoryManager.CycleInventoryUp;
        cycleDown.performed -= inventoryManager.CycleInventoryDown;
    }
}
