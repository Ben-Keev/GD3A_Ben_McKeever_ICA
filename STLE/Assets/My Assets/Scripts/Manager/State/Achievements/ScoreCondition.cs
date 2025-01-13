using UnityEngine;

namespace GD.State
{
    /// <summary>
    /// Checks whether a player's score is above a certain threshold
    /// </summary>
    [CreateAssetMenu(fileName = "ScoreCondition", menuName = "GD/Conditions/Single/Score", order = 5)]
    public class ScoreCondition : ConditionBase
    {
        [Tooltip("The Player's Score Tracker")]
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