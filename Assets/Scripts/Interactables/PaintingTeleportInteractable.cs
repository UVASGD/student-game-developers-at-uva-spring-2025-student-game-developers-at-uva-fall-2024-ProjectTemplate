using System;
using UnityEngine;

[RequireComponent(typeof(TeleportingIllusionBase))]
public class PaintingTeleportInteractable : GameplayUIInteractable
{
    private TeleportingIllusionBase teleportingIllusion;
    public override void Start()
    {
        base.Start();
        teleportingIllusion = GetComponent<TeleportingIllusionBase>();
    }
    public override void Interact()
    {
        gameplayUI.activate(() => { teleportingIllusion.teleportObject(player); });
    }
}
