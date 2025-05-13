using UnityEngine;
using UnityEngine.UI;

public class SettingsManagerSimple : MonoBehaviour
{
    public AudioSource masterSource; // Tüm ses bu ise sadece bunu kullan
    public AudioSource musicSource;
    public AudioSource ambienceSource;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider ambienceSlider;


    void Start()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        ambienceSlider.onValueChanged.AddListener(SetAmbienceVolume);
    }

    public void SetMasterVolume(float value)
    {
        if (masterSource != null)
            masterSource.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetAmbienceVolume(float value)
    {
        if (ambienceSource != null)
            ambienceSource.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
}