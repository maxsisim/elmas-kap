using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public AudioClip splashSound;      // Inspector'dan atanacak
    [Range(0f, 1f)] public float volume = 1.5f; // Ses seviyesi (Inspector'dan ayarlanabilir)
    private string nextSceneName = "MainMenuScene";  // Ge�ilecek sahne ad�

    private AudioSource audioSource;

    void Start()
    {
        // AudioSource olu�tur ve ayarla
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = splashSound;
        audioSource.volume = volume;
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Sesi �al
        audioSource.Play();

        // Sesin s�resi kadar bekle ve sahneyi y�kle
        Invoke("LoadNextScene", splashSound.length);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
