using GD.Audio;
using GD.Types;
using Sirenix.OdinInspector;
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

    [FoldoutGroup("Grade Fanfares")]
    [SerializeField]
    [Tooltip("0 is highest grade. Lowest possible is 7")]
    private AudioClip[] fanfares = new AudioClip[8];

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
            case >= 200:
                AudioManager.Instance.PlaySound(fanfares[0], AudioMixerGroupName.Background, true, true);
                return "A++";
            case >= 175:
                AudioManager.Instance.PlaySound(fanfares[0], AudioMixerGroupName.Background, true, true);
                return "A+";
            case >= 150:
                AudioManager.Instance.PlaySound(fanfares[1], AudioMixerGroupName.Background, true, true);
                return "A";
            case >= 100:
                AudioManager.Instance.PlaySound(fanfares[2], AudioMixerGroupName.Background, true, true);
                return "B";
            case >= 60:
                AudioManager.Instance.PlaySound(fanfares[3], AudioMixerGroupName.Background, true, true);
                return "C";
            case >= 30:
                AudioManager.Instance.PlaySound(fanfares[4], AudioMixerGroupName.Background, true, true);
                return "D";
            case >= 0:
                AudioManager.Instance.PlaySound(fanfares[5], AudioMixerGroupName.Background, true, true);
                return "E";
            default:
                AudioManager.Instance.PlaySound(fanfares[6], AudioMixerGroupName.Background, true, true);
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
