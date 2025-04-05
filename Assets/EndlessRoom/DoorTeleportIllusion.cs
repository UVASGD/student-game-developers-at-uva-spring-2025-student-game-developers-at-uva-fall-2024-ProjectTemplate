using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleportIllusion : Interactable {    
    public Transform destination;
    private Vector3 test;

    public override void Interact() {
        GameObject player = EndlessRoomManager.Instance.player;
        Vector3 positionOffset = player.transform.position - transform.position;
        // Rotate positionOffset by difference between transform's rotation and rotation of destination
        positionOffset = Quaternion.Inverse(transform.rotation) * positionOffset;
        positionOffset = destination.rotation * positionOffset;

        player.transform.position = destination.position + positionOffset;
        // rotate player rotation by difference between transform's rotation and rotation of destination
        player.transform.rotation = destination.rotation * Quaternion.Inverse(transform.rotation) * player.transform.rotation;

        test = player.transform.position;
        // This is weird bug, I have to call Test() in next frame to get correct position otherwise I clip out of plane somehow??
        Invoke("Test", 0.00001f);
    }

    void Test(){
        GameObject player = EndlessRoomManager.Instance.player;
        player.transform.position = test;
        Debug.Log(player.transform.position);
    }

    public override void SetText()
    {
        base.SetText();
        InteractableUI.Instance.interactUI_Text.text = "Open";
    }
}