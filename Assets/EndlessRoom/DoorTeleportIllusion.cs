using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorTeleportIllusion : Door {    
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
        // player.transform.rotation = destination.rotation * Quaternion.Inverse(transform.rotation) * player.transform.rotation;
        player.transform.rotation = player.transform.rotation * Quaternion.Euler(0, 90, 0); // kinda jank, lazy hard-coded solution for my specific one case in which I use this class
        test = player.transform.position;
        EndlessRoomManager.Instance.mazeGenerator.CreateMaze();
        EndlessRoomManager.Instance.ActivateMazeBoard();
        // This is weird bug, I have to call Test() in next frame to get correct position otherwise I clip out of plane somehow??
        Invoke("DelayedMove", 0.00001f);
    }

    void DelayedMove(){
        GameObject player = EndlessRoomManager.Instance.player;
        player.transform.position = test;
        if (destination.GetComponent<Door>() != null){
            Door illusionDoor = destination.GetComponent<Door>();
            illusionDoor.doorOpenSFX.Play();
            illusionDoor.canInteract = false;
            illusionDoor.doorLeftRotatorTransform.rotation = Quaternion.Euler(0, illusionDoor._startYRotationLeft, 0);
            illusionDoor.doorRightRotatorTransform.rotation = Quaternion.Euler(0, illusionDoor._startYRotationRight, 0);
            illusionDoor.doorLeftRotatorTransform.DORotate(new Vector3(0, -90, 0), 1f);
            illusionDoor.doorRightRotatorTransform.DORotate(new Vector3(0, 90, 0), 1f);
        }
        Debug.Log(player.transform.position);
    }

    public override void SetText()
    {
        base.SetText();
        InteractableUI.Instance.interactUI_Text.text = "Open";
    }
}