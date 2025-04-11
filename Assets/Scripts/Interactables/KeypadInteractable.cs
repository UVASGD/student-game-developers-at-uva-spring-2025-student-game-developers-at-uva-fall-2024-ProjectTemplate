using UnityEngine;

public class KeypadInteractable : GameplayUIInteractable
{

    public override void Interact()
    {
        gameplayUI.activate();
    }

}
