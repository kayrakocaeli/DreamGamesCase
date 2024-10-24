using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject confirmationDialog;
    [SerializeField] private Button levelButton;
    [SerializeField] private Button clearPrefsButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private TextMeshProUGUI levelText;
    
    private void Awake()
    {
        clearPrefsButton.onClick.AddListener(ShowConfirmationDialog);
        yesButton.onClick.AddListener(ClearPlayerPrefs);
        noButton.onClick.AddListener(HideConfirmationDialog);
        int level = PlayerPrefs.GetInt("Level", 1);
        if (level > 10)
        {
            levelText.text = "Finished";
            levelButton.CancelInvoke();

        }
        else
        {
            levelText.text = "Level " + level;

            levelButton.onClick.RemoveAllListeners();
            levelButton.onClick.AddListener(() => GameManager.Instance.LoadLevelScene());
        }
    }
    
    private void ShowConfirmationDialog()
    {
        confirmationDialog.SetActive(true);
    }

    private void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        int level = PlayerPrefs.GetInt("Level", 1);
        levelText.text = "Level " + level;
        confirmationDialog.SetActive(false);
    }

    private void HideConfirmationDialog()
    {
        confirmationDialog.SetActive(false);
    }
}
