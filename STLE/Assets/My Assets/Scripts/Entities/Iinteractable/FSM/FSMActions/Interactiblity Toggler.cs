using GD.FSM;
using Unity.VisualScripting;
using UnityEngine;
using System.Reflection;
using System;
using GD.Items;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu(menuName = "GD/FSM/Action/ToggleInteractibility")]

// Can enable or disable aspects of the player's controls.
public class ToggleInteractibility : FSMAction
{
    [Tooltip("The target object to measure distance from.")]
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private bool enabled;

    override public void Execute(GameObject context = null)
    {
        context.GetComponent<IInteractable>().SetInteractable(enabled);
    }
}
