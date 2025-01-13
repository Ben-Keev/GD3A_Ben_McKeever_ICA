using GD.Events;
using TMPro;
using UnityEngine;

namespace GD.State
{
    /// <summary>
    /// Runs a timer
    /// Returns true if the timer runs out.
    /// Triggers "Danger Mode" at a given time where the timer has nearly ran out.
    /// </summary>
    [CreateAssetMenu(fileName = "Stopwatch", menuName = "GD/Conditions/Single/Time", order = 5)]
    public class Stopwatch : ConditionBase
    {
        [Tooltip("Time in Seconds to fulfill condition")]
        [SerializeField]
        private float startingTime;

        [Tooltip("At what point should the danger music cut in")]
        [SerializeField]
        private float dangerThreshold;

        [Tooltip("Event raised when about to run out of time")]
        [SerializeField]
        private GameEvent timeRunningOut;

        private bool dangerThresholdReached;
        private float timeLeft;
        private bool timerOn = false;

        public float StartingTime { get => startingTime; set => startingTime = value; }
        public float TimeLeft { get => timeLeft; set => timeLeft = value; }
        public bool TimerOn { get => timerOn; set => timerOn = value; }
        public bool DangerThresholdReached { get => dangerThresholdReached; set => dangerThresholdReached = value; }

        // https://www.youtube.com/watch?v=hxpUk0qiRGs
        // Implement countdown guide
        protected override bool EvaluateCondition(ConditionContext conditionContext)
        {
            if (TimerOn)
            {
                if (timeLeft > 0) // Reduce timeLeft while it's greater than 0
                    timeLeft -= Time.deltaTime; 

                if(timeLeft < dangerThreshold && !dangerThresholdReached) // There's less time left than danger threshold
                {
                    timeRunningOut?.Raise();
                    DangerThresholdReached = true; // Ensures this if block is only tiggered once.
                }

                return timeLeft < 0;
            }

            return false; // Can't run out of time if timer isn't running
        }

        /// <summary>
        /// Convert the remaining time to a string.
        /// </summary>
        /// <param name="currentTime">The time you want to be converted</param>
        /// <returns>Time in the format M:SS which can be easily printed</returns>
        public string ConvertTimeToString(float currentTime)
        {
            currentTime += 1;

            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            return string.Format("{0:00} : {1:00}", minutes, seconds);
        }
    }
}