using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Noteboard : Interactable {  
    public Transform noteboardViewTransform;
    public float interactBufferTime = 0.25f;
    private float _timer = 0f;

    public override void Interact() {
        // Open up read note UI or just freeze camera towards note
        if (canInteract) {
            EndlessRoomManager.Instance.player.GetComponent<PlayerMovement>().lockMovement();
            EndlessRoomManager.Instance.player.GetComponent<PlayerCameraMovement>().lockPan();
            Camera.main.transform.DOMove(noteboardViewTransform.position, EndlessRoomManager.Instance.cameraMoveZoomTime);
            Camera.main.transform.DORotate(noteboardViewTransform.rotation.eulerAngles, EndlessRoomManager.Instance.cameraMoveZoomTime);
            canInteract = false;
            _timer = 0f;
        }
    }

    public override void SetText()
    {
        base.SetText();
        InteractableUI.Instance.interactUI_Text.text = "Read";
    }

    public override void Update()
    {
        base.Update();
        _timer += Time.deltaTime;
        if (!canInteract && Input.GetKeyDown(KeyCode.E) && _timer > interactBufferTime) {
            _timer = 0f;
            Camera.main.transform.DOLocalMove(Vector3.zero, EndlessRoomManager.Instance.cameraMoveZoomTime);
            Camera.main.transform.DORotate(EndlessRoomManager.Instance.player.transform.rotation.eulerAngles, EndlessRoomManager.Instance.cameraMoveZoomTime).OnComplete(
                () => {
                    EndlessRoomManager.Instance.player.GetComponent<PlayerMovement>().unlockMovement();
                    EndlessRoomManager.Instance.player.GetComponent<PlayerCameraMovement>().unlockPan();
                }
            );
            canInteract = true;
        }
    }
}