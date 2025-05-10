using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : Interactable {    
    public Transform doorLeftRotatorTransform;
    public Transform doorRightRotatorTransform;
    [HideInInspector] public float _startYRotationLeft;
    [HideInInspector] public float _startYRotationRight;
    public AudioSource doorOpenSFX;
    public int doorNum = 0;

    public override void Start()
    {
        base.Start();
        _startYRotationLeft = doorLeftRotatorTransform.rotation.eulerAngles.y;
        _startYRotationRight = doorRightRotatorTransform.rotation.eulerAngles.y;
    }

    public override void Interact() {
        base.Interact();
        canInteract = false;
        AudioManager.audioManagerInstance.PlaySFX(AudioManager.audioManagerInstance.openDoor);
        Debug.Log("Test");
        doorLeftRotatorTransform.DORotate(new Vector3(0, _startYRotationLeft + 90, 0), 1f);
        doorRightRotatorTransform.DORotate(new Vector3(0, _startYRotationRight - 90, 0), 1f);

        if (doorNum == 1) 
        {
            AudioManager.audioManagerInstance.StopMusic();
            AudioManager.audioManagerInstance.PlayMusic(AudioManager.audioManagerInstance.endlessRoomsLevelBackground2);
        }
    }

    public override void SetText()
    {
        base.SetText();
        InteractableUI.Instance.interactUI_Text.text = "Open";
    }
}