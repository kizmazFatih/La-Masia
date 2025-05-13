using UnityEngine;
using UnityEngine.UI;

public class DisplaySettingsManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Slider brightnessSlider;

    void Start()
    {
        // Tam ekran ayarını yükle
        fullscreenToggle.isOn = Screen.fullScreen;

        // Parlaklık ayarını yükle
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        brightnessSlider.value = savedBrightness;
        ApplyBrightness(savedBrightness);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Resolution currentResolution = Screen.currentResolution;

        if (isFullscreen)
        {
            // Ekranı tam ekran yap ve mevcut çözünürlüğü uygula
            Screen.SetResolution(currentResolution.width, currentResolution.height, FullScreenMode.FullScreenWindow);
        }
        else
        {
            // Pencere moduna geç
            Screen.SetResolution(currentResolution.width, currentResolution.height, FullScreenMode.Windowed);
        }
    }

    public void SetBrightness(float brightness)
    {
        ApplyBrightness(brightness);
        PlayerPrefs.SetFloat("Brightness", brightness);
    }

    private void ApplyBrightness(float value)
    {
        // Sahne ortam ışığını değiştir
        RenderSettings.ambientLight = new Color(value, value, value, 1f);
    }
}