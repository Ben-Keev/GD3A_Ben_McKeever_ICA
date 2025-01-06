using GD.Types;
using UnityEngine;

namespace GD.FSM
{
    /// <summary>
    /// Abstract class for predicates used in FSM transitions. Each predicate evaluates
    /// a specific condition to determine if a transition should occur.
    /// </summary>
    public abstract class PredicateBase : ScriptableGameObject
    {
        public abstract bool Evaluate(GameObject context = null);
    }
}