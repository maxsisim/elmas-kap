using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsScript : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Önceki sesi PlayerPrefs’ten çek (varsa), yoksa 1f (tam ses)
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        // Slider dinleyici ekle
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume); // Kaydet
    }
}
