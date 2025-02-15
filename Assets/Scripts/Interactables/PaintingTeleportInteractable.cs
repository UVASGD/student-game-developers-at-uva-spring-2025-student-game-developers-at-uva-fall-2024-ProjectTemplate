using UnityEngine;

public class PaintingTeleportInteractable : TestInteractable
{
    [SerializeField] private GameplayUI paintingGameplayUI;
    public override void Interact()
    {
        usePainting();
    }

    private void usePainting()
    {
        paintingGameplayUI.activate();
    }
}
