using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    public AudioClip newClip;

    void Start()
    {
        GameObject musicManager = GameObject.Find("MusicManager");
        if (musicManager != null)
        {
            AudioSource source = musicManager.GetComponent<AudioSource>();
            if (source.clip != newClip)
            {
                source.clip = newClip;
                source.Play();
            }
        }
    }
}
