using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Toggle musicToggle;
    public Toggle soundToggle;
    public Dropdown resolutionDropdown;

    private bool isMusicOn = true;
    private bool isSoundOn = true;
    private Resolution[] resolutions;

    private void Start()
    {
        // Toggle butonları ayarla
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        soundToggle.onValueChanged.AddListener(ToggleSound);
        
        // Çözünürlük seçeneklerini ayarla
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // Çözünürlükleri dropdown menüye ekle
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolutions[i].width + "x" + resolutions[i].height));
        }

        // Mevcut çözünürlüğü seçili hale getir
        resolutionDropdown.value = resolutions.Length - 1;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
    }

    private void ToggleMusic(bool isOn)
    {
        isMusicOn = isOn;
        AudioListener.volume = isMusicOn ? 1 : 0;
    }

    private void ToggleSound(bool isOn)
    {
        isSoundOn = isOn;
        // Ses efektlerini kapatmak için buraya ekleme yapabilirsiniz.
    }

    private void ChangeResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, true);
    }
}