using UnityEngine;

public class GameplayUIInteractable : Interactable
{
    [SerializeField] protected GameplayUI gameplayUI;
    protected GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //must be a better way to get player
        if (player != null)
        {
            gameplayUI.setPlayer(player);
        }
        else
        {
            Debug.LogWarning("interactable could not find player");
        }
    }

    public override void Interact()
    {

    }
}
