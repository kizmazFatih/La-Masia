using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject settingsButton;
    public GameObject exitButton;
    public GameObject settingsPanel;

    public void StartGame()
    {
        HideAllButtons();
        SceneManager.LoadScene(1);
        Debug.Log("Game started");
    }

    public void OpenSettings()
    {
        HideAllButtons();
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        HideAllButtons();
        Debug.Log("Oyun kapatılıyor...");
        Application.Quit();
    }

    private void HideAllButtons()
    {
        startButton.SetActive(false);
        settingsButton.SetActive(false);
        exitButton.SetActive(false);
    }

    private void ShowMainButtons()
    {
        startButton.SetActive(true);
        settingsButton.SetActive(true);
        exitButton.SetActive(true);
    }
}