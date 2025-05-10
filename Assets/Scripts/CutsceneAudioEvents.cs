using UnityEngine;

public class CutsceneAudioEvents : MonoBehaviour
{
    public void PlayBossFired()
    {
        AudioManager.audioManagerInstance.PlaySFXLowerMusic(AudioManager.audioManagerInstance.bossFired);
    }

    public void PlayBossMuffled()
    {
        AudioManager.audioManagerInstance.PlaySFXLowerMusic(AudioManager.audioManagerInstance.bossMuffled);
    }

    public void PlayWakeup()
    {
        AudioManager.audioManagerInstance.PlaySFXLowerMusic(AudioManager.audioManagerInstance.wakeup);
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
        AudioManager.audioManagerInstance.PlaySFXLowerMusic(AudioManager.audioManagerInstance.yippee);
    }
}
