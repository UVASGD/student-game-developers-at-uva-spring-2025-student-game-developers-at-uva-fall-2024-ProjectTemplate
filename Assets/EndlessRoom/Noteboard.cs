using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Noteboard : Interactable {  
    public Transform noteboardViewTransform;

    public override void Interact() {
        // Open up read note UI or just freeze camera towards note
        if (canInteract) {
            EndlessRoomManager.Instance.player.GetComponent<PlayerMovement>().enabled = false;
            EndlessRoomManager.Instance.player.GetComponent<PlayerCameraMovement>().enabled = false;
            Camera.main.transform.DOMove(noteboardViewTransform.position, EndlessRoomManager.Instance.cameraMoveZoomTime);
            Camera.main.transform.DORotate(noteboardViewTransform.rotation.eulerAngles, EndlessRoomManager.Instance.cameraMoveZoomTime);
            canInteract = false;
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
        if (!canInteract && Input.GetKeyDown(KeyCode.E)) {
            Camera.main.transform.DOMove(EndlessRoomManager.Instance.player.transform.position, EndlessRoomManager.Instance.cameraMoveZoomTime);
            Camera.main.transform.DORotate(EndlessRoomManager.Instance.player.transform.rotation.eulerAngles, EndlessRoomManager.Instance.cameraMoveZoomTime).OnComplete(
                () => {
                    EndlessRoomManager.Instance.player.GetComponent<PlayerMovement>().enabled = true;
                    EndlessRoomManager.Instance.player.GetComponent<PlayerCameraMovement>().enabled = true;
                }
            );
            canInteract = true;
        }
    }
}