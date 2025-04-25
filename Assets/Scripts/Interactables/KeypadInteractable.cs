using UnityEngine;

public class KeyPadInteractable : Interactable
{


    public override void Interact()
    {

    }

    public override void SetText()
    {
        base.SetText();
        InteractableUI.Instance.interactUI_Text.text = "Interact";
    }

    private void OnApplicationQuit()
    {
        // reset keypad
    }
}
