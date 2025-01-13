using GD.FSM;
using UnityEngine;
using GD.Items;

[CreateAssetMenu(menuName = "GD/FSM/Action/ToggleInteractibility")]

/// <summary>
/// Enables or disales an IInteractable's ability to be interacted with.
/// </summary>
public class ToggleInteractability : FSMAction
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
