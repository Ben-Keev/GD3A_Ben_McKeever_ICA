using UnityEngine;

namespace GD.State
{
    /// <summary>
    /// A condition that ensures no objects with a specific tag or layer are nearby.
    /// </summary>
    [CreateAssetMenu(fileName = "ScoreCondition", menuName = "GD/Conditions/Single/Score", order = 5)]
    public class ScoreCondition : ConditionBase
    {
        [Tooltip("The Score Tracker")]
        [SerializeField]
        private ScoreTracker scoreTracker;

        [Tooltip("The Score Needed to fulfill condition")]
        [SerializeField]
        private int threshold;

        protected override bool EvaluateCondition(ConditionContext conditionContext)
        {
            return scoreTracker.Score >= threshold;
        }
    }
}