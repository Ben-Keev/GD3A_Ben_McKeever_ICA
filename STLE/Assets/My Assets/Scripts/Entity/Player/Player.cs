using UnityEngine;

public class Player : MonoBehaviour
{

    public ScoreTracker scoreTracker;

    private PlayerDialogueInputHandler dialogueInputHandler;
    private PlayerExploreInputHandler exploreInputHandler;

    private void Awake()
    {
        scoreTracker.InitialiseScore();
        dialogueInputHandler = GetComponent<PlayerDialogueInputHandler>();
        exploreInputHandler = GetComponent<PlayerExploreInputHandler>();
    }

    public void toggleExplore(bool exploreEnabled)
    {
        exploreInputHandler.enabled = exploreEnabled;
        dialogueInputHandler.enabled = !exploreEnabled;
    }
}
