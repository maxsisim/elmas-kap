using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public AudioClip splashSound;      // Inspector'dan atanacak
    [Range(0f, 1f)] public float volume = 1.5f; // Ses seviyesi (Inspector'dan ayarlanabilir)
    private string nextSceneName = "MainMenuScene";  // Geçilecek sahne adý

    private AudioSource audioSource;

    void Start()
    {
        // AudioSource oluþtur ve ayarla
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = splashSound;
        audioSource.volume = volume;
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Sesi çal
        audioSource.Play();

        // Sesin süresi kadar bekle ve sahneyi yükle
        Invoke("LoadNextScene", splashSound.length);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
