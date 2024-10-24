using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons; 
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button sfxButton;      
    [SerializeField] private Button musicButton;    
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToMainMenu;

    [SerializeField] private AudioSource sfxSource; 
    [SerializeField] private AudioSource musicSource;
    
    [SerializeField] private Sprite sfxOnSprite;    
    [SerializeField] private Sprite sfxOffSprite;   
    [SerializeField] private Sprite musicOnSprite;  
    [SerializeField] private Sprite musicOffSprite;

    [SerializeField] private GameObject SettingsOnPanel;

    [SerializeField] private float delayBetweenButtons = 0.2f; 
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private float rotationDuration = 0.5f;
    [SerializeField] private bool isMenuOpen = false;

    void Start()
    {
        settingsButton.onClick.AddListener(ToggleSettingsMenu);
        sfxButton.onClick.AddListener(ToggleSFX);
        musicButton.onClick.AddListener(ToggleMusic);
        restartButton.onClick.AddListener(RestartLevel);
        backToMainMenu.onClick.AddListener(BackToMainMenu);
    }

    public void ToggleSettingsMenu()
    {
        AudioManager.Instance.PlayEffect(SoundID.Cube);
        isMenuOpen = !isMenuOpen;
        SettingsOnPanel.SetActive(isMenuOpen);

        settingsButton.transform.DORotate(isMenuOpen ? new Vector3(0, 0, 180) : new Vector3(0, 0, 0), rotationDuration);

        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform rectTransform = buttons[i].GetComponent<RectTransform>();

            if (isMenuOpen)
            {
                rectTransform.DOAnchorPosX(-123, moveDuration).SetDelay(i * delayBetweenButtons);
            }
            else
            {
                rectTransform.DOAnchorPosX(200, moveDuration).SetDelay(i * delayBetweenButtons);
            }
        }
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.PlayEffect(SoundID.Cube);
        sfxSource.mute = !sfxSource.mute;

        if (sfxSource.mute)
        {
            sfxButton.image.sprite = sfxOffSprite;
        }
        else
        {
            sfxButton.image.sprite = sfxOnSprite;
        }
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.PlayEffect(SoundID.Cube);
        musicSource.mute = !musicSource.mute;

        if (musicSource.mute)
        {
            musicButton.image.sprite = musicOffSprite;
        }
        else
        {
            musicButton.image.sprite = musicOnSprite;
        }
    }

    public void RestartLevel()
    {
        AudioManager.Instance.PlayEffect(SoundID.Cube);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
