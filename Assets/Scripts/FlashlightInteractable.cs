using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FlashlightInteractable : Interactable
{
    [SerializeField] private List<GameObject> disable;
    [SerializeField] private List<GameObject> enable;
    [SerializeField] private List<QuantumObjectBase> quantumConsiderLit;
    [SerializeField] private GameObject flashlightPrefab;
    [SerializeField] private Transform newRespawnPos;
    [SerializeField] private WeepingAngel weep;
    public float delaySeconds = 0.5f;

    private GameObject player;
    public override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player");
    }
    public override void Interact()
    {
        if(player != null)
        {
            AudioManager.audioManagerInstance.PlaySFX(AudioManager.audioManagerInstance.flashlight);

            Flashlight script = player.AddComponent<Flashlight>();
            
            GameObject flashlightBeam = Instantiate(flashlightPrefab, player.transform.Find("Head"));
            script.flashlightBeam = flashlightBeam;
            foreach (GameObject go in disable)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in enable)
            {
                go.SetActive(true);
            }
            foreach (QuantumObjectBase qob in quantumConsiderLit)
            {
                qob.considerLit = true;
            }
            RenderSettings.ambientIntensity = 0f;
            RenderSettings.ambientLight = Color.black;
            RenderSettings.reflectionIntensity = 0f;
            if(weep != null)
            {
                weep.playerRespawnPos = newRespawnPos;
            }
            Destroy(gameObject);

            AudioManager.audioManagerInstance.PlaySFX(AudioManager.audioManagerInstance.powerDown);
            AudioManager.audioManagerInstance.StopMusic();
            AudioManager.audioManagerInstance.PlayMusic(AudioManager.audioManagerInstance.flashlightLevelBackground2);
        }
        else
        {
            Debug.LogWarning("cant find player");
        }
    }
}
