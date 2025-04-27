using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightInteractable : Interactable
{
    [SerializeField] private List<GameObject> lights;
    [SerializeField] private GameObject flashlightPrefab;
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
            Flashlight script = player.AddComponent<Flashlight>();
            
            GameObject flashlightBeam = Instantiate(flashlightPrefab, player.transform.Find("Head"));
            script.flashlightBeam = flashlightBeam;
            foreach (GameObject go in lights)
            {
                go.SetActive(false);
            }
            RenderSettings.ambientIntensity = 0f;
            RenderSettings.ambientLight = Color.black;
            RenderSettings.reflectionIntensity = 0f;
        }
        else
        {
            Debug.LogWarning("cant find player");
        }
    }
}
