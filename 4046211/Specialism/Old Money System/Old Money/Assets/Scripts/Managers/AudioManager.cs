using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;

    public static AudioManager Instance; // Singleton

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void PlayAudio(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}