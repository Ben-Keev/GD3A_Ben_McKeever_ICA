using GD.Events;
using TMPro;
using UnityEngine;

namespace GD.State
{
    /// <summary>
    /// A condition that ensures no objects with a specific tag or layer are nearby.
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
        protected override bool EvaluateCondition(ConditionContext conditionContext)
        {
            if (TimerOn)
            {
                if (timeLeft > 0)
                    timeLeft -= Time.deltaTime;

                if(timeLeft < dangerThreshold && !dangerThresholdReached)
                {
                    timeRunningOut?.Raise();
                    DangerThresholdReached = true;
                }

                return timeLeft < 0;
            }

            return false; // Can't run out of time if timer isn't running
        }

        public string ConvertTimeToString(float currentTime)
        {
            currentTime += 1;

            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            return string.Format("{0:00} : {1:00}", minutes, seconds);
        }
    }
}