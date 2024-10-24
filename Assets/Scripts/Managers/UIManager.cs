using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Threading;

public class UIManager : Singleton<UIManager>
{ 
    [SerializeField] private TextMeshProUGUI levelNumberTextCompleted;
    [SerializeField] private TextMeshProUGUI levelNumberTextFailed;

    [SerializeField] private GameObject levelCompletedPanel;
    [SerializeField] private Button nextButton;

    [SerializeField] private GameObject levelFailedPanel;
    [SerializeField] private Button retryButton;

    [SerializeField] private Image characterImage;

    private void OnEnable()
    {
        MovesManager.Instance.OnMovesFinished += CheckGoals;
    }
    private void OnDisable()
    {
        if (MovesManager.Instance != null)
            MovesManager.Instance.OnMovesFinished -= CheckGoals;
    }

    private void CheckGoals()
    {
        if (!GoalManager.Instance.CheckAllGoalsCompleted())
        {
            SetLostPanel();
        }
    }

    public void SetLevelCompletedPanel()
    {
        levelNumberTextCompleted.text = "Level " + PlayerPrefs.GetInt("Level", 1);
        levelCompletedPanel.SetActive(true);
        nextButton.onClick.RemoveAllListeners();
        
        if (GameManager.Instance == null)
            throw new System.Exception("ILLEGAL STATE: Please load the game from MainScene.");

        nextButton.onClick.AddListener(() => GameManager.Instance.NextLevel());
        characterImage.transform.DOScale(1.5f, 1f);
        characterImage.transform.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360);
    }

    public void SetLostPanel()
    {
        levelNumberTextFailed.text = "Level " + PlayerPrefs.GetInt("Level", 1);
        levelFailedPanel.SetActive(true);
        
        if (GameManager.Instance == null)
            throw new System.Exception("ILLEGAL STATE: Please load the game from MainScene.");

        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() => GameManager.Instance.LoadLevelScene());
    }
}
