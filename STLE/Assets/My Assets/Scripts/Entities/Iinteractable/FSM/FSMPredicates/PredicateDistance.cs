using GD.FSM;
using UnityEngine;

/// <summary>
/// Refactored from DistanceCondition.cs to inherit from Predicate Base
/// A condition that ensures prefabs are spawned farther than a minimum distance from a target.
/// </summary>
[CreateAssetMenu(fileName = "PredicateDistance", menuName = "GD/FSM/Predicate/PredicateDistance")]
public class PredicateDistance : PredicateBase
{

    [Tooltip("The minimum distance required to set as true.")]
    [SerializeField]
    private float minDistance;

    [Tooltip("Check if it's in range or not.")]
    [SerializeField]
    private bool inRange;

    override public bool Evaluate(GameObject context = null)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        return Vector3.Distance(player.transform.position, context.transform.position) > minDistance == inRange;
    }
}
