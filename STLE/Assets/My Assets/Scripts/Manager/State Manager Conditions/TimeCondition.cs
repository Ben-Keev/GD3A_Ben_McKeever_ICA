using UnityEngine;

namespace GD.State
{
    /// <summary>
    /// A condition that ensures no objects with a specific tag or layer are nearby.
    /// </summary>
    [CreateAssetMenu(fileName = "TimeCondition", menuName = "GD/Conditions/Single/Time", order = 5)]
    public class TimeCondition : ConditionBase
    {
        [Tooltip("Time in Seconds to fulfill condition")]
        [SerializeField]
        private int threshold;

        protected override bool EvaluateCondition(ConditionContext conditionContext)
        {
              return Time.timeSinceLevelLoad >= threshold;
        }
    }
}