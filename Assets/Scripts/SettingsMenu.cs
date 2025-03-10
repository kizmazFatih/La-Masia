using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public CanvasGroup mainMenuUI;

    public void OpenSettings()
    {
        settingsPanel.SetActive(true); // Paneli aç
        mainMenuUI.alpha = 0; // Ana menüyü görünmez yap
        mainMenuUI.interactable = false; // Butonlara tıklanmasını engelle
        mainMenuUI.blocksRaycasts = false;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Paneli kapat
        mainMenuUI.alpha = 1; // Ana menüyü tekrar görünür yap
        mainMenuUI.interactable = true; // Butonlara tıklanabilir hale getir
        mainMenuUI.blocksRaycasts = true;
    }
}