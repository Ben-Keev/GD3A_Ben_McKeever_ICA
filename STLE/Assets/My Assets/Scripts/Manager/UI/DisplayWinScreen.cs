using GD.Audio;
using GD.Types;
using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Displays final grade card
/// </summary>
public class DisplayWinScreen : MonoBehaviour
{
    [FoldoutGroup("Data", expanded: true)]
    [SerializeField]
    [Tooltip("The score to be read to the screen.")]
    private ScoreTracker scoreTracker;

    [FoldoutGroup("Data", expanded: true)]
    [Tooltip("All possible bins that can exist")]
    [SerializeField]
    private BinData[] bins;

    [FoldoutGroup("Audio")]
    [SerializeField]
    [Tooltip("Fanfare that accomponies grade. 0 is highest 7 is lowest")]
    private AudioClip[] fanfares = new AudioClip[8];

    /// <summary>
    /// Display HighScore in a text component
    /// </summary>
    private void UpdateHighScore()
    {
        TextMeshProUGUI textComponent = transform.Find("Dynamic Content/High Score").GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
            textComponent.text = scoreTracker.HighScore.ToString();
    }

    /// <summary>
    /// Display score in a text component
    /// </summary>
    private void UpdateScore()
    {
        TextMeshProUGUI textComponent = transform.Find("Dynamic Content/Score").GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
            textComponent.text = scoreTracker.Score.ToString();
    }

    /// <summary>
    /// Display the grade in a text component
    /// </summary>
    private void UpdateGrade()
    {
        TextMeshProUGUI textComponent = transform.Find("Dynamic Content/Grade").GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            string grade = CalculateGrade(scoreTracker.Score);
            textComponent.text = grade;
        }
    }

    /// <summary>
    /// Calculate grade based on given score
    /// </summary>
    /// <param name="score">Score taken from ScoreTracker</param>
    /// <returns>A string containing the grade letter</returns>
    private string CalculateGrade(int score)
    {
        switch (score)
        {
            case >= 300:
                AudioManager.Instance.PlaySound(fanfares[0], AudioMixerGroupName.Background, true, true);
                return "A++";
            case >= 225:
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

    /// <summary>
    /// Display breakdown of 1) Correct item placed in a bin and 2) incorrect item placed in a bin
    /// </summary>
    /// <param name="bin"></param>
    private void UpdateBin(BinData bin)
    {
        string correctPath = $"Dynamic Content/{bin.BinType}/Correct";
        string wrongPath = $"Dynamic Content/{bin.BinType}/Wrong";

        TextMeshProUGUI correctTextDisplay = transform.Find(correctPath)?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI wrongTextDisplay = transform.Find(wrongPath)?.GetComponent<TextMeshProUGUI>();

        Tuple<int, int> binPerformance = scoreTracker.GetBinPerformance(bin);

        if (correctTextDisplay != null)
        {
            correctTextDisplay.text = binPerformance.Item1.ToString();
        }

        if (wrongTextDisplay != null)
        {
            wrongTextDisplay.text = binPerformance.Item2.ToString();
        }
    }

    /// <summary>
    /// Update all child components displaying score
    /// </summary>
    public void UpdateAll()
    {
        UIManager.Instance.IndicatorEmpty.SetActive(false);

        scoreTracker.StoreHighScore();
        UpdateHighScore();
        UpdateScore();
        UpdateGrade();

        foreach (var bin in bins)
        {
            UpdateBin(bin);
        }
    }

    /// <summary>
    /// Whenever panel is shown update the data
    /// </summary>
    private void OnEnable()
    {
        UpdateAll();
    }
}
