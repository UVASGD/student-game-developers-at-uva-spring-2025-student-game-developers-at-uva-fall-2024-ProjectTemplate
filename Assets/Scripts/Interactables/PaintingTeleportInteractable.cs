using UnityEngine;

[RequireComponent(typeof(TeleportingIllusionBase))]
public class PaintingTeleportInteractable : TestInteractable
{
    [SerializeField] private GameplayUI paintingGameplayUI;
    private TeleportingIllusionBase teleportingIllusion;
    private GameObject player;
    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //must be a better way to get player
        if (player != null)
        {
            paintingGameplayUI.setPlayer(player);
        }
        else
        {
            Debug.LogWarning("Painting teleport interactable could not find player");
        }
        teleportingIllusion = GetComponent<TeleportingIllusionBase>();
    }
    public override void Interact()
    {
        usePainting();
    }

    private void usePainting()
    {
        paintingGameplayUI.activate();

        teleportingIllusion.teleportObject(player); //if there is interacting animation, this should be delayed
        

    }
}
