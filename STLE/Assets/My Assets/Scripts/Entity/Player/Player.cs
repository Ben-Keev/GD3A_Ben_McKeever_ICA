using UnityEngine;
using GD.State;

public class Player : MonoBehaviour
{
    [SerializeField]
    private ScoreTracker scoreTracker;

    [SerializeField]
    private Stopwatch stopWatch;

    private PlayerDialogueInputHandler dialogueInputHandler;
    private PlayerExploreInputHandler exploreInputHandler;

    public ScoreTracker ScoreTracker { get => scoreTracker; set => scoreTracker = value; }
    public Stopwatch StopWatch { get => stopWatch; set => stopWatch = value; }

    private void Awake()
    {
        ScoreTracker.InitialiseScore();
        dialogueInputHandler = GetComponent<PlayerDialogueInputHandler>();
        exploreInputHandler = GetComponent<PlayerExploreInputHandler>();
    }

    public void toggleExplore(bool exploreEnabled)
    {
        exploreInputHandler.enabled = exploreEnabled;
        dialogueInputHandler.enabled = !exploreEnabled;
    }
}
