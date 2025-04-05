using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {    
    public override void Interact() {
        base.Interact();
        gameObject.SetActive(false);
        canInteract = false;
        Debug.Log("Replace with door animation later.");
    }

    public override void SetText()
    {
        base.SetText();
        InteractableUI.Instance.interactUI_Text.text = "Open";
    }
}