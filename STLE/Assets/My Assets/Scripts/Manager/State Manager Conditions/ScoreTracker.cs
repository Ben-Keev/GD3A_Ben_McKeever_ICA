using UnityEngine;

[CreateAssetMenu(menuName = "GD/Data/ScoreTracker")]

public class ScoreTracker : ScriptableObject
{
    public int score;

    // Total number of items picked up throughout the game.
    public int lifetimeTrashCollected;
    public int lifetimeRecyclableCollected;
    public int lifetimeCompostableCollected;

    public void InitialiseScore()
    {
        score = 0;
        lifetimeTrashCollected = 0;
        lifetimeRecyclableCollected = 0;
        lifetimeCompostableCollected = 0;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }
}
