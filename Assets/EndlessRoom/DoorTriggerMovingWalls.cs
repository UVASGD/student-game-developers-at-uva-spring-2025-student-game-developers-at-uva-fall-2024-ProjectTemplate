using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerMovingWalls : Door {   
    public WallMove[] wallsToMove;
    

    public override void Interact() {
        base.Interact();
        foreach(WallMove wall in wallsToMove){
            wall.StartMoving();
        }
        EndlessRoomManager.Instance.wallMovingSFX.Play();
        EndlessRoomManager.Instance.psychLoopNoBassSFX.Stop();
        EndlessRoomManager.Instance.psychLoopBassSFX.Play();
    }
}