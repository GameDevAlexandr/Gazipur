using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using Zenject;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button startButton, settingsButton, authorsButton, backSettingsButton, backAuthorsButton, exitButton;
    [SerializeField] private GameObject settingsPanel, authorsPanel, buttonPanel;

    private Sounds _sounds;

    [Inject]
    private void Construct(Sounds sounds)
    {
        _sounds = sounds;
    }

    void Start()
    {
        startButton.onClick.AddListener(() => OnButtonClick(0));
        settingsButton.onClick.AddListener(() => OnButtonClick(0));
        authorsButton.onClick.AddListener(() => OnButtonClick(0));
        backSettingsButton.onClick.AddListener(() => OnButtonClick(0));
        backAuthorsButton.onClick.AddListener(() => OnButtonClick(0));
        exitButton.onClick.AddListener(() => OnButtonClick(0));

        startButton.onClick.AddListener(OnStartGame);
        settingsButton.onClick.AddListener(OnOpenSettings);
        authorsButton.onClick.AddListener(OnOpenAuthors);
        backSettingsButton.onClick.AddListener(OnBack);
        backAuthorsButton.onClick.AddListener(OnBack);
        exitButton.onClick.AddListener(OnExit);

        buttonPanel.SetActive(true);
        settingsPanel.SetActive(false);
        authorsPanel.SetActive(false);
    }

    private void OnButtonClick(int soundType)
    {
        if (_sounds != null)
            _sounds.ButtonClick(soundType);
    }

    private void OnStartGame() { SceneManager.LoadSceneAsync(1); }
    private void OnOpenSettings() { buttonPanel.SetActive(false); settingsPanel.SetActive(true); authorsPanel.SetActive(false); }
    private void OnOpenAuthors() { buttonPanel.SetActive(false); settingsPanel.SetActive(false); authorsPanel.SetActive(true); }
    private void OnBack() { buttonPanel.SetActive(true); settingsPanel.SetActive(false); authorsPanel.SetActive(false); }
    private void OnExit() { Application.Quit(); }
}