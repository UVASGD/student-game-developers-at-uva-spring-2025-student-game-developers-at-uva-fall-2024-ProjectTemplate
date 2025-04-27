using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightInteractable : Interactable
{
    [SerializeField] private List<GameObject> disable;
    [SerializeField] private List<GameObject> enable;
    [SerializeField] private List<QuantumObjectBase> quantumConsiderLit;
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
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("cant find player");
        }
    }
}
