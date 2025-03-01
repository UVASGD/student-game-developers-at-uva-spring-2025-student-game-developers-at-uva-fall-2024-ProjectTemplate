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
    private Vector3 _originalPosition;

    void Awake(){
        _originalPosition = transform.position;
    }

    public void StartMoving(){
        transform.DOMove(moveToTransform.position, totalMoveTime);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Player"){
            GameObject player = other.gameObject;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            player.GetComponent<PlayerCameraMovement>().enabled = false;
            fader.gameObject.SetActive(true);
            fader.DOFade(1f, 1f).OnComplete(() => {
                player.transform.position = playerRespawnPoint.position;
                transform.position = _originalPosition;
                fader.DOFade(0f, 1f).OnComplete(() => {
                    fader.gameObject.SetActive(false);
                    player.GetComponent<PlayerMovement>().enabled = true;
                    player.GetComponent<PlayerCameraMovement>().enabled = true;
                });
            });
        }
    }
}