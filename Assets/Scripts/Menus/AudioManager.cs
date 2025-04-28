using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

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
    public AudioClip footsteps;

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
        PlayMusic(menuBackground);
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
