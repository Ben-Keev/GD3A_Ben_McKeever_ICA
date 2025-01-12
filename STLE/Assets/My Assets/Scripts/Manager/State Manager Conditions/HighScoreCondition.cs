using UnityEngine;

namespace GD.State
{
    /// <summary>
    /// A condition that ensures no objects with a specific tag or layer are nearby.
    /// </summary>
    [CreateAssetMenu(fileName = "HighScoreCondition", menuName = "GD/Conditions/Single/HighScore", order = 6)]
    public class HighScoreCondition : ConditionBase
    {
        [Tooltip("The Score Tracker")]
        [SerializeField]
        private ScoreTracker scoreTracker;

        protected override bool EvaluateCondition(ConditionContext conditionContext)
        {
            return scoreTracker.Score > scoreTracker.HighScore;
        }
    }
}