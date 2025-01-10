using UnityEngine;

[CreateAssetMenu(menuName = "GD/Data/ScoreTracker")]

public class ScoreTracker : ScriptableObject
{
    [SerializeField]
    private int score;

    // Total number of items picked up throughout the game.
    [SerializeField]
    private int lifetimeTrashCollected;
    [SerializeField]
    private int lifetimeRecyclableCollected;
    [SerializeField]
    private int lifetimeCompostableCollected;

    public int Score { get => score; set => score = value; }
    public int LifetimeTrashCollected { get => lifetimeTrashCollected; set => lifetimeTrashCollected = value; }
    public int LifetimeRecyclableCollected { get => lifetimeRecyclableCollected; set => lifetimeRecyclableCollected = value; }
    public int LifetimeCompostableCollected { get => lifetimeCompostableCollected; set => lifetimeCompostableCollected = value; }

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
