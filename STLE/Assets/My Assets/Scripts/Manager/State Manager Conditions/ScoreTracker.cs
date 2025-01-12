using GD.Types;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GD/Data/ScoreTracker")]

public class ScoreTracker : ScriptableObject
{
    [SerializeField]
    private int score;

    // Saved between sessions
    private int highScore;

    public int Score { get => score; set => score = value; }
    public int HighScore { get => highScore; set => highScore = value; }


    public void InitialiseScore()
    {
        score = 0;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    public void StoreHighScore()
    {
        if (highScore < score)
            highScore = score;
    }

    public Tuple<int, int> GetBinPerformance(BinData bin)
    {
        int total = bin.BinContents.Tally();
        int correctTally = bin.BinContents.Count(bin.AcceptedItem);
        int wrongTally = total - correctTally;

        return new Tuple<int, int>(correctTally, wrongTally);
    }
}
