using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Button resetButton;
    private MusicManager musicManager;

    void Start()
    {
        musicManager = MusicManager.GetInstance();

        if (resetButton != null)
        {
            resetButton.onClick.RemoveAllListeners();
            resetButton.onClick.AddListener(ToggleMusic);
        }
    }

    void ToggleMusic()
    {
        if (musicManager != null && musicManager.audioSource != null)
        {
            AudioSource audio = musicManager.audioSource;

            if (audio.isPlaying)
            {
                audio.Pause();
                Debug.Log("Müzik durduruldu.");
            }
            else
            {
                audio.Play();
                Debug.Log("Müzik devam ettirildi.");
            }
        }
        else
        {
            Debug.LogWarning("MusicManager veya AudioSource null!");
        }
    }
}
