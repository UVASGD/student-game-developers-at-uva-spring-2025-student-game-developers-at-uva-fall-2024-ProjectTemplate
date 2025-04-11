using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip menuBackgroundMusic;
    public AudioClip inGameBackgroundMusic;
    public AudioClip button;
    public AudioClip pickup;

    public static AudioManager audioManagerInstance;

    private void Awake()
    {
        if (audioManagerInstance == null)
        {
            audioManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(menuBackgroundMusic);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource != null && musicClip != null)
        { 
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxSource != null && sfxClip != null)
        {
            sfxSource.clip = sfxClip;
            sfxSource.PlayOneShot(sfxClip);
        }
    }

    public void StopMusic()
    {
        if(musicSource != null)
        { 
            musicSource.Stop(); 
        }
    }
}
