using GD;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Better than storing arbitrary UI variables across random scripts
/// </summary>
public class UIManager : Singleton<UIManager>
{
    [FoldoutGroup("Indicators")]
    [Tooltip("Parent containing all indicators")]
    [SerializeField]
    private GameObject indicatorsParent;

    [FoldoutGroup("Indicators")]
    [Tooltip("UI displaying Stopwatch")]
    [SerializeField]
    private TextMeshProUGUI stopwatch;

    [FoldoutGroup("Indicators")]
    [Tooltip("UI displaying Score")]
    [SerializeField]
    private TextMeshProUGUI score;

    [FoldoutGroup("Indicators")]
    [SerializeField]
    [InlineEditor]
    [Tooltip("The UI Component indicating the selected inventory")]
    private Image selectedInventory;

    [FoldoutGroup("Indicators")]
    [SerializeField]
    [InlineEditor]
    [Tooltip("The UI Component indicating how much is left in the selected inventory")]
    private TextMeshProUGUI itemsLeft;

    [FoldoutGroup("Feedback")]
    [Tooltip("UI displaying winning screen")]
    [SerializeField]
    private GameObject feedbackParent;

    public GameObject IndicatorEmpty { get => indicatorsParent; set => indicatorsParent = value; }
    public TextMeshProUGUI Stopwatch { get => stopwatch; set => stopwatch = value; }
    public TextMeshProUGUI Score { get => score; set => score = value; }
    public Image SelectedInventory { get => selectedInventory; set => selectedInventory = value; }
    public TextMeshProUGUI ItemsLeft { get => itemsLeft; set => itemsLeft = value; }
    public GameObject FeedbackScreen { get => feedbackParent; set => feedbackParent = value; }
}
