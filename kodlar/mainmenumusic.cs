using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public AudioSource audioSource;

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;
        public AudioClip musicClip;
    }

    [Header("Sahneye özel müzikler")]
    public List<SceneMusic> sceneMusicList;

    private const string VolumeKey = "MusicVolume";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            // Başlangıçta kaydedilmiş sesi uygula
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.7f);
            audioSource.volume = savedVolume;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    void PlayMusicForScene(string sceneName)
    {
        SceneMusic found = sceneMusicList.Find(x => x.sceneName == sceneName);

        if (found != null && found.musicClip != null)
        {
            if (audioSource.clip != found.musicClip)
            {
                audioSource.clip = found.musicClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        // ❌ BU SATIR GEREKSİZ, ÇIKARILDI:
        // float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        // audioSource.volume = savedVolume;
    }

    public void RefreshVolume()
    {
        if (audioSource != null)
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
            audioSource.volume = savedVolume;
        }
    }

    public static MusicManager GetInstance() => instance;
    public static bool InstanceExists() => instance != null;
}
