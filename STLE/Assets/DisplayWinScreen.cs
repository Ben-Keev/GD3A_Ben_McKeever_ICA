using System;
using TMPro;
using UnityEngine;

public class DisplayWinScreen : MonoBehaviour
{
    [SerializeField]
    private ScoreTracker scoreTracker; // Injected score tracker
    [SerializeField]
    private BinData[] bins; // Array of bins (Trash, Recycle, Compost)
    [SerializeField]
    private GameObject gameUI;

    // Method to update High Score
    private void UpdateHighScore()
    {
        TextMeshProUGUI textComponent = transform.Find("Dynamic Content/High Score").GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
            textComponent.text = scoreTracker.HighScore.ToString();
    }

    // Method to update Score
    private void UpdateScore()
    {
        TextMeshProUGUI textComponent = transform.Find("Dynamic Content/Score").GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
            textComponent.text = scoreTracker.Score.ToString();
    }

    // Method to calculate and update Grade
    private void UpdateGrade()
    {
        TextMeshProUGUI textComponent = transform.Find("Dynamic Content/Grade").GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            string grade = CalculateGrade(scoreTracker.Score);
            textComponent.text = grade;
        }
    }

    // Grade calculation based on the score
    private string CalculateGrade(int score)
    {
        switch (score)
        {
            case >= 3:
                return "A";
            case >= 2:
                return "B";
            case >= 1:
                return "C";
            case >= 0:
                return "D";
            case >= -1:
                return "E";
            default:
                return "F";
        }
    }

    // Unified method to update bins (Trash, Recycle, Compost)
    private void UpdateBin(BinData bin)
    {
        string correctPath = $"Dynamic Content/{bin.BinType}/Correct";
        string wrongPath = $"Dynamic Content/{bin.BinType}/Wrong";

        TextMeshProUGUI correctText = transform.Find(correctPath)?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI wrongText = transform.Find(wrongPath)?.GetComponent<TextMeshProUGUI>();

        Tuple<int, int> binPerformance = scoreTracker.GetBinPerformance(bin);

        if (correctText != null)
        {
            correctText.text = binPerformance.Item1.ToString();
        }

        if (wrongText != null)
        {
            wrongText.text = binPerformance.Item2.ToString();
        }
    }

    // Method to update all components
    public void UpdateAll()
    {
        gameUI.SetActive(false);

        scoreTracker.StoreHighScore();
        UpdateHighScore();
        UpdateScore();
        UpdateGrade();

        foreach (var bin in bins)
        {
            UpdateBin(bin);
        }
    }

    // Whenever the panel is shown update the data
    private void OnEnable()
    {
        UpdateAll();
    }
}
