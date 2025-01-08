using GD.FSM;
using Unity.VisualScripting;
using UnityEngine;
using System.Reflection;
using System;

[CreateAssetMenu(menuName = "GD/FSM/Action/InputSwapper")]

// Can enable or disable aspects of the player's controls.
public class InputMapSwapper : FSMAction
{
    [SerializeField]
    private bool enableExplore;

    override public void Execute(GameObject context = null)
    {

        context.GetComponent<Player>().toggleExplore(enableExplore);
    }
}
