using UnityEngine;

namespace GD.State
{
    /// <summary>
    /// Checks if the current high score has been beaten
    /// </summary>
    [CreateAssetMenu(fileName = "HighScoreCondition", menuName = "GD/Conditions/Single/HighScore", order = 6)]
    public class HighScoreCondition : ConditionBase
    {
        [Tooltip("The Player's Score Tracker")]
        [SerializeField]
        private ScoreTracker scoreTracker;

        protected override bool EvaluateCondition(ConditionContext conditionContext)
        {
            return scoreTracker.Score > scoreTracker.HighScore;
        }
    }
}