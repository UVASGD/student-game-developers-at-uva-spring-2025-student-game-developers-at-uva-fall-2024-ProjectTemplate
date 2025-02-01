using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable {    
    public override void Update() {
        if (Input.GetKeyDown(KeyCode.I)){
            base.Update();
            Interact();
        }
    }
    public override void Interact() {
        Debug.Log("Interacted with TestInteractable");
    }
}