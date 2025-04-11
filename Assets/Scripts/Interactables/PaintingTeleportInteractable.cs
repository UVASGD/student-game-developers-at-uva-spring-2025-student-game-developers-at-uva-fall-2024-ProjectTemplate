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
        usePainting();
    }

    private void usePainting()
    {
        gameplayUI.activate();

        teleportingIllusion.teleportObject(player); //if there is interacting animation, this should be delayed
    }
}
