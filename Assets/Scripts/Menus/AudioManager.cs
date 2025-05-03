using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioMixer audioMixer;

    [Header("Audio Clips")]
    [Header("Menus")]
    public AudioClip menuBackground;
    public AudioClip button;

    [Header("Paintings Level")]
    public AudioClip paintingsLevelBackground;
    public AudioClip generalUFO;
    public AudioClip ufoLaser;
    public AudioClip angryUFO;
    // public AudioClip pickup;
    // public AudioClip openDoor;

    [Header("Endless Rooms Level")]
    public AudioClip endlessRoomsLevelBackground1;
    public AudioClip endlessRoomsLevelBackground2;
    // public AudioClip jumpscare;
    // public AudioClip pickup;
    // public AudioClip openDoor;
    public AudioClip movingWalls;
    public AudioClip lightbulb;

    [Header("Flashlight Level")]
    public AudioClip flashlightLevelBackground1;
    public AudioClip flashlightLevelBackground2;
    public AudioClip flashlight;
    public AudioClip powerDown;
    // public AudioClip jumpscare;
    // public AudioClip pickup;
    public AudioClip keypadButton;
    public AudioClip keypadDenied;
    public AudioClip keypadGranted;
    public AudioClip vineboom; 
    public AudioClip footsteps;

    [Header("Cutscene")]
    public AudioClip cutsceneBackground;
    public AudioClip creditsBackground;
    public AudioClip yippee;
    public AudioClip bossFired;
    public AudioClip bossMuffled;
    public AudioClip wakeup;

    [Header("Misc")]
    public AudioClip pickup;
    public AudioClip openDoor;
    public AudioClip jumpscare;

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
        // Read the initial music volume (saved in PlayerPrefs)
        originalMusicVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                PlayMusic(menuBackground);
                break;
            case 1:
                PlayMusic(paintingsLevelBackground);
                break;
            case 2:
                PlayMusic(endlessRoomsLevelBackground1);
                break;
            case 3:
                PlayMusic(flashlightLevelBackground1);
                break;
            case 4:
                PlayMusic(cutsceneBackground);
                break;
        }
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

    public void PlaySFXRepeat(AudioClip sfxClip)
    {
        if (sfxSource != null && sfxClip != null)
        {
            sfxSource.clip = sfxClip;
            sfxSource.loop = true;
            sfxSource.Play();
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

    public void StopSFX()
    {
        if (sfxSource != null)
        {
            sfxSource.Stop();
        }
    }

    [Range(0f, 1f)] public float loweredMusicVolumeMultiplier = 0.1f; // Multiplier of original
    public float volumeRestoreDelay = 1.0f; // Seconds to wait after SFX to restore
    private float originalMusicVolume; // In 0-1 range
    private Coroutine volumeRestoreCoroutine;

    private void LowerMusicVolumeTemporarily()
    {
        // Lower music volume
        float loweredVolume = originalMusicVolume * loweredMusicVolumeMultiplier;
        audioMixer.SetFloat("music_volume", SliderToDb(loweredVolume));

        if (volumeRestoreCoroutine != null)
        {
            StopCoroutine(volumeRestoreCoroutine);
        }
        volumeRestoreCoroutine = StartCoroutine(RestoreMusicVolumeAfterDelay());
    }

    private IEnumerator RestoreMusicVolumeAfterDelay()
    {
        yield return new WaitForSeconds(volumeRestoreDelay);

        // Restore original music volume
        audioMixer.SetFloat("music_volume", SliderToDb(originalMusicVolume));
    }

    public void PlaySFXLowerMusic(AudioClip sfxClip)
    {
        if (sfxSource != null && sfxClip != null)
        {
            LowerMusicVolumeTemporarily();
            sfxSource.PlayOneShot(sfxClip);
        }
    }

    public float SliderToDb(float value)
    {
        return Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
    }
}
