using UnityEngine;
using GD.State;
using Sirenix.OdinInspector;

/// <summary>
/// Holds information about the player's context. Useful when passed through State Manager.
/// </summary>
public class Player : MonoBehaviour
{
    [FoldoutGroup("Data", expanded: true)]
    [Tooltip("Scoretracker for player's performance")]
    [SerializeField]
    private ScoreTracker scoreTracker;

    [FoldoutGroup("Data", expanded: true)]
    [Tooltip("Scoretracker for player's performance")]
    [SerializeField]
    private Stopwatch stopWatch;

    private PlayerDialogueInputHandler dialogueInputHandler;
    private PlayerExploreInputHandler exploreInputHandler;

    public ScoreTracker ScoreTracker { get => scoreTracker; set => scoreTracker = value; }
    public Stopwatch StopWatch { get => stopWatch; set => stopWatch = value; }

    private void Awake()
    {
        // Reset the player's score from the last session.
        ScoreTracker.InitialiseScore();
        dialogueInputHandler = GetComponent<PlayerDialogueInputHandler>();
        exploreInputHandler = GetComponent<PlayerExploreInputHandler>();
    }

    /// <summary>
    /// Swaps between Dialogue inputs and Explore inputs.
    /// </summary>
    /// <param name="exploreEnabled">True = explore enabled and dialogue disabled</param>
    public void toggleExplore(bool exploreEnabled)
    {
        exploreInputHandler.enabled = exploreEnabled;
        dialogueInputHandler.enabled = !exploreEnabled;
    }
}
