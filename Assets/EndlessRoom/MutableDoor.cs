using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class MutableDoor : MonoBehaviour {
    public float playerDistToDoorTriggerCreak;
    public float playerDistToDoorTriggerKill;
    public Transform doorRotatorTransform;
    public AudioSource doorCreakSFX;
    public AudioSource doorKillSFX;
    public Transform cameraPointToTransform;
    private bool _hasDoorCreakTriggered = false;
    private bool _hasDoorKillTriggered = false;
    [HideInInspector] public float _startYRotation;

    void Start()
    {
        _startYRotation = doorRotatorTransform.rotation.eulerAngles.y;
    }

    void Update(){
        if (Vector3.Distance(EndlessRoomManager.Instance.player.transform.position, transform.position) < playerDistToDoorTriggerKill && !_hasDoorKillTriggered) {
            //Kill();
        } else if (Vector3.Distance(EndlessRoomManager.Instance.player.transform.position, transform.position) < playerDistToDoorTriggerCreak && !_hasDoorCreakTriggered) {
            //Creak(); Illusion is broken because if the door creaks, its corresponding duplicate needs to creak too
        }
    }

    void Creak(){
        if (doorCreakSFX != null) doorCreakSFX.Play();
        doorRotatorTransform.DORotate(new Vector3(0, _startYRotation + 30, 0), 1f);
        _hasDoorCreakTriggered = true;
    }

    void Kill(){
        if (doorKillSFX != null) doorKillSFX.Play();
        doorRotatorTransform.DORotate(new Vector3(0, _startYRotation + 90, 0), 0.5f);
        _hasDoorKillTriggered = true;

        EndlessRoomManager.Instance.player.GetComponent<PlayerMovement>().enabled = false;
        EndlessRoomManager.Instance.player.GetComponent<PlayerCameraMovement>().enabled = false;
        Camera.main.transform.DORotate(cameraPointToTransform.rotation.eulerAngles, 0.5f);
        // For now, delay time is 2 seconds, but it should be set to the length of the death animation
        Camera.main.transform.DOMove(cameraPointToTransform.position, 0.5f).SetDelay(2f).OnComplete(() => {
            ResetDoors();
            TriggerFader();
        });
    }

    void ResetDoors(){
        MutableDoor[] mutableDoors = FindObjectsOfType<MutableDoor>();
        foreach (MutableDoor mutableDoor in mutableDoors) {
            mutableDoor._hasDoorCreakTriggered = false;
            mutableDoor._hasDoorKillTriggered = false;
            mutableDoor.doorRotatorTransform.rotation = Quaternion.Euler(0, mutableDoor._startYRotation, 0);
        }
    }

    void TriggerFader(){
        EndlessRoomManager.Instance.fader.gameObject.SetActive(true);
        EndlessRoomManager.Instance.fader.DOFade(1f, 1f).OnComplete(() => {
            EndlessRoomManager.Instance.player.transform.position = EndlessRoomManager.Instance.spawnTransformSection2.position;
            EndlessRoomManager.Instance.player.transform.rotation = EndlessRoomManager.Instance.spawnTransformSection2.rotation;
            EndlessRoomManager.Instance.fader.DOFade(0f, 1f).OnComplete(() => {
                EndlessRoomManager.Instance.fader.gameObject.SetActive(false);
                EndlessRoomManager.Instance.player.GetComponent<PlayerMovement>().enabled = true;
                EndlessRoomManager.Instance.player.GetComponent<PlayerCameraMovement>().enabled = true;          
                Camera.main.transform.localRotation = Quaternion.identity;
                Camera.main.transform.localPosition = Vector3.zero;          
            });
        });
    }
}