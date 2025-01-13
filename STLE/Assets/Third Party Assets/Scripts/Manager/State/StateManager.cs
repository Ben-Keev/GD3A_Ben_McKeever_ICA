using GD.Audio;
using GD.FSM;
using GD.Items;
using GD.Tick;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GD.State
{
    /// <summary>
    /// Manages the game state by evaluating win and loss conditions.
    /// </summary>
    public class StateManager : MonoBehaviour, IHandleTicks
    {
        [FoldoutGroup("Timing & Reset", expanded: true)]
        [SerializeField]
        [Tooltip("The tick rate type for the state manager (i.e. multiple of baseTickIntervalSecs)")]
        private TimeTickSystem.TickRateMultiplierType tickRateType
    = TimeTickSystem.TickRateMultiplierType.BaseInterval;

        [FoldoutGroup("Timing & Reset")]
        [SerializeField]
        [Tooltip("Reset all conditions on start")]
        private bool resetAllConditionsOnStart = true;

        [FoldoutGroup("Context", expanded: true)]
        [SerializeField]
        [Tooltip("Player reference to evaluate conditions required by the context")]
        private Player player;

        // Gotten from player
        private FSMController playerFSM;

        [FoldoutGroup("Context")]
        [SerializeField]
        [Tooltip("Player inventory collection to evaluate conditions required by the context")]
        private InventoryCollection inventoryCollection;

        /// <summary>
        /// The condition that determines if the player wins.
        /// </summary>
        [FoldoutGroup("Conditions")]
        [SerializeField]
        [Tooltip("The condition that determines if the player wins")]
        private ConditionBase winCondition;

        /// <summary>
        /// The condition that determines if the player loses.
        /// </summary>
        [FoldoutGroup("Conditions")]
        [SerializeField]
        [Tooltip("The condition that determines if the player loses")]
        private ConditionBase loseCondition;

        [FoldoutGroup("Tutorial")]
        [SerializeField]
        [Tooltip("Bins that will appear when you exit the tutorial")]
        private GameObject gameplayBinsParent;

        [FoldoutGroup("Tutorial")]
        [SerializeField]
        [Tooltip("Items that will appear when you exit the tutorial")]
        private GameObject gameplayItemsParent;

        [FoldoutGroup("Tutorial")]
        [SerializeField]
        [Tooltip("The helper sign that introduces you to the game")]
        private NPC helperSign;

        [FoldoutGroup("Tutorial")]
        [SerializeField]
        [Tooltip("The helper sign's dialogue")]
        private string[] tutorialText = new string[5];

        [FoldoutGroup("Tutorial")]
        [SerializeField]
        [Tooltip("Explanation of danger mode")]
        private string[] dangerText = new string[5];

        [FoldoutGroup("Stopwatch")]
        [SerializeField]
        [Tooltip("Determines if the player is out of time")]
        private Stopwatch stopwatch;

        [FoldoutGroup("Music")]
        [SerializeField]
        [Tooltip("Plays during tutorial")]
        private AudioClip tutorialMusic;

        [FoldoutGroup("Music")]
        [SerializeField]
        [Tooltip("Plays once timer starts")]
        private AudioClip inGameMusic;

        [FoldoutGroup("Music")]
        [SerializeField]
        [Tooltip("Plays when entering danger")]
        private AudioClip dangerModeMusic;

        [FoldoutGroup("Achievements [optional]")]
        [SerializeField]
        [Tooltip("Set of optional conditions related to acheivements")]
        private List<ConditionBase> achievementConditions;

        /// <summary>
        /// The game is ended
        /// </summary>
        private bool gameEnded = false;

        /// <summary>
        /// Player is in tutorial
        /// </summary>
        private bool inTutorial = true;

        private ConditionContext conditionContext;
        private void Awake()
        {
            if (player == null)
                throw new System.Exception("Player reference is required!");

            if (inventoryCollection == null)
                throw new System.Exception("Inventory collection reference is required!");

            // Wrap the two objects inside the context envelope
            conditionContext = new ConditionContext(player, inventoryCollection);

            // Register with the tick system
            TimeTickSystem.Instance.RegisterListener(tickRateType, HandleTick);

            InitialiseTutorial();
        }

        private void InitialiseTutorial()
        {
            UIManager.Instance.FeedbackScreen.SetActive(false);
            gameplayBinsParent.SetActive(false);
            gameplayItemsParent.SetActive(false);

            helperSign.NpcData.OverwriteDialogue(tutorialText);

            stopwatch.TimeLeft = stopwatch.StartingTime;
            stopwatch.DangerThresholdReached = false;
            stopwatch.TimerOn = false;
        }

        public void ToggleTutorial(bool enabled)
        {
            inTutorial = !enabled;
            gameplayItemsParent.SetActive(enabled);
            gameplayBinsParent.SetActive(enabled);
            stopwatch.TimerOn = enabled;

            AudioManager.Instance.PlaySound(inGameMusic, Types.AudioMixerGroupName.Background, true, true);
        }

        private void OnDestroy()
        {
            // Unregister with the tick system
            TimeTickSystem.Instance.UnregisterListener(tickRateType, HandleTick);
        }

        private void Start()
        {
            if (resetAllConditionsOnStart)
                ResetConditions();
            
            AudioManager.Instance.PlaySound(tutorialMusic, Types.AudioMixerGroupName.Background, true, true);

            // Initiates the first cutscene.
            helperSign.GetComponent<NPC>().Interact(gameObject);
        }

        /// <summary>
        /// Evaluates conditions each frame and handles game state transitions.
        /// </summary>
        private void Update()
        {
            if (!inTutorial)
            {

                if (stopwatch != null && stopwatch.Evaluate(conditionContext) && !gameEnded)
                {
                    TransitionToResultsScreen();
                    gameEnded = true;
                }

                UIManager.Instance.Score.text = player.ScoreTracker.Score.ToString();
                UIManager.Instance.Stopwatch.text = stopwatch.ConvertTimeToString(stopwatch.TimeLeft);

            }
        }

        /// <summary>
        /// Explains Danger mode to the player and changes music.
        /// </summary>
        public void EnterDangerThreshold()
        {
            AudioManager.Instance.PlaySound(dangerModeMusic, Types.AudioMixerGroupName.Background, true, false);

            helperSign.NpcData.OverwriteDialogue(dangerText);
            helperSign.GetComponent<NPC>().Interact(gameObject);
        }

        private void TransitionToResultsScreen()
        {
            player.gameObject.GetComponent<FSMController>().enabled = false;
            player.gameObject.GetComponent<PlayerExploreInputHandler>().enabled = false;
            UIManager.Instance.FeedbackScreen.SetActive(true);
        }

        /// <summary>
        /// Resets the win and loss conditions.
        /// Call this method when restarting the game or level.
        /// </summary>
        public void ResetConditions()
        {
            // Reset the gameEnded flag
            gameEnded = false;

            // Reset the win condition
            if (winCondition != null)
                winCondition.ResetCondition();

            // Reset the lose condition
            if (loseCondition != null)
                loseCondition.ResetCondition();

            // Reset the achievement conditions
            if (achievementConditions != null)
            {
                foreach (var achievmentCondition in achievementConditions)
                {
                    if (achievmentCondition != null)
                        achievmentCondition.ResetCondition();
                }
            }
        }

        /// <summary>
        /// Move code from Update to HandleTick to perform the tasks at a slower rate
        /// The timer doesn
        /// </summary>
        /// <see cref="TimeTickSystem"/>
        public void HandleTick()
        {
            if (!inTutorial)
            {
                // If the game has already ended, no need to evaluate further
                if (gameEnded)
                    return;



                foreach (var achievmentCondition in achievementConditions)
                {
                    if (achievmentCondition != null && achievmentCondition.Evaluate(conditionContext))
                    {
                        if (achievmentCondition.Name == "BeatHighScore")
                            UIManager.Instance.NewHighScore.enabled = true;
                    }
                }
            }
        }
    }
}