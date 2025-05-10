using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WallMove : MonoBehaviour {    
    public Transform moveToTransform;
    public float totalMoveTime = 10f;
    public Transform playerRespawnPoint;
    public Image fader;
    [HideInInspector] public Vector3 originalPosition;

    void Awake(){
        originalPosition = transform.position;
    }

    public void StartMoving(){
        transform.DOMove(moveToTransform.position, totalMoveTime);
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Player")){
            EndlessRoomManager.Instance.numWallsHittingEndSegment++;
            Debug.Log(EndlessRoomManager.Instance.numWallsHittingEndSegment);
            if (EndlessRoomManager.Instance.numWallsHittingEndSegment >= 2){
                PlayerRespawn();
            }
        }
    }

    void OnCollisionExit(Collision other){
        if (other.gameObject.CompareTag("Player")){
            EndlessRoomManager.Instance.numWallsHittingEndSegment--;
        }
    }

    void PlayerRespawn(){
        EndlessRoomManager.Instance.numWallsHittingEndSegment = 0;
        GameObject player = EndlessRoomManager.Instance.player;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.GetComponent<PlayerCameraMovement>().enabled = false;
        fader.gameObject.SetActive(true);
        fader.DOFade(1f, 1f).OnComplete(() => {
            // kill DOMOve tween
            foreach (WallMove wall in EndlessRoomManager.Instance.doorTriggerMovingWalls.wallsToMove){
                wall.transform.DOKill();
                wall.transform.position = wall.originalPosition;
            }
            player.transform.position = playerRespawnPoint.position;
            player.transform.rotation = playerRespawnPoint.rotation;
            EndlessRoomManager.Instance.doorTriggerMovingWalls.gameObject.SetActive(true);
            EndlessRoomManager.Instance.doorTriggerMovingWalls.canInteract = true;
            fader.gameObject.SetActive(false);
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerCameraMovement>().enabled = true;
            EndlessRoomManager.Instance.numWallsHittingEndSegment = 0;
        });
    }
}