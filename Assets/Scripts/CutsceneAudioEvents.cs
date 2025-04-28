using UnityEngine;

public class CutsceneAudioEvents : MonoBehaviour
{
    public void Start()
    {
        AudioManager.audioManagerInstance.StopMusic();
        PlayCutsceneMusic();
    }
    public void PlayBossFired()
    {
        AudioManager.audioManagerInstance.PlaySFX(AudioManager.audioManagerInstance.bossFired);
    }

    public void PlayBossMuffled()
    {
        AudioManager.audioManagerInstance.PlaySFX(AudioManager.audioManagerInstance.bossMuffled);
    }

    public void PlayWakeup()
    {
        AudioManager.audioManagerInstance.PlaySFX(AudioManager.audioManagerInstance.wakeup);
    }

    public void PlayCreditsMusic()
    {
        AudioManager.audioManagerInstance.StopMusic();
        AudioManager.audioManagerInstance.PlayMusic(AudioManager.audioManagerInstance.creditsBackground);
    }

    public void PlayCutsceneMusic()
    {
        AudioManager.audioManagerInstance.PlayMusic(AudioManager.audioManagerInstance.cutsceneBackground);
    }

    public void PlayThankYouSFX()
    {
        AudioManager.audioManagerInstance.PlaySFX(AudioManager.audioManagerInstance.yippee);
    }
}
