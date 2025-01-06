using GD.FSM;
using Unity.VisualScripting;
using UnityEngine;
using System.Reflection;
using System;

[CreateAssetMenu(menuName = "GD/FSM/Action/InputSwapper")]

// Can enable or disable aspects of the player's controls.
public class InputMapSwapper : FSMAction
{
    private GameObject player;
    [SerializeField]
    private InputHandlerType inputHandlerType;
    [SerializeField]
    private bool enabled;

    override public void Execute(GameObject context = null)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        switch (inputHandlerType)
        {
            case InputHandlerType.ExploreInputHandler:
                player.GetComponent<PlayerExploreInputHandler>().enabled = enabled; break;
            case InputHandlerType.DialogueInputHandler:
                player.GetComponent<PlayerDialogueInputHandler>().enabled = enabled; break;
        }
    }
}
