using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORTANT:
// All GameObjects containing Interactable script or its child classes should have their layer set to "Interactable" for interactable system to work
// These GameObjects must also have a 3D collider to determine their interact hitbox
public class Interactable : MonoBehaviour {
    public bool canInteract = true;
    
    public virtual void Start() {}
    public virtual void Update() {}
    public virtual void Interact() {}
    public virtual void SetText() {}
}