using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable {    
    public override void Interact() {
        Debug.Log("Interacted with TestInteractable");
    }

    public override void SetText()
    {
        base.SetText();
        InteractableUI.Instance.interactUI_Text.text = "Interact";
    }
}