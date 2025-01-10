using GD.FSM;
using Unity.VisualScripting;
using UnityEngine;
using System.Reflection;
using System;
using GD.Events;

[CreateAssetMenu(menuName = "GD/FSM/Action/InputSwapper")]

// Can enable or disable aspects of the player's controls.
public class InputMapSwapper : FSMAction
{
    [SerializeField]
    private bool enableExplore;

    [SerializeField]
    private BoolGameEvent swapEvent;

    override public void Execute(GameObject context = null)
    {
        context.GetComponent<Player>().toggleExplore(enableExplore);
        swapEvent?.Raise(enableExplore);
    }
}
