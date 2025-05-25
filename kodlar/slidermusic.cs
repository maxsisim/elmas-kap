using UnityEngine;
using UnityEngine.UI;

public class SliderAudio : MonoBehaviour
{
    public GameObject audioSettingsMenu; // Ses ayarları paneli
    public Slider volumeSlider;           // Ses seviyesi slider'ı

    private AudioSource audioSource;
    private const string VolumeKey = "MusicVolume"; // MusicManager ile aynı anahtar

    void Start()
    {
        // MusicManager varsa AudioSource'u al
        if (MusicManager.InstanceExists())
        {
            audioSource = MusicManager.GetInstance().audioSource;

            // Kaydedilen ses seviyesini uygula
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
            audioSource.volume = savedVolume;

            if (volumeSlider != null)
            {
                volumeSlider.value = savedVolume;
                volumeSlider.onValueChanged.AddListener(ChangeVolume);
            }
        }

        // Menü başta kapalı olsun
        if (audioSettingsMenu != null)
        {
            audioSettingsMenu.SetActive(false);
        }
    }

    public void ChangeVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }

        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void OpenAudioMenu()
    {
        if (audioSettingsMenu != null)
            audioSettingsMenu.SetActive(true);
    }

    public void CloseAudioMenu()
    {
        if (audioSettingsMenu != null)
            audioSettingsMenu.SetActive(false);
    }
}
