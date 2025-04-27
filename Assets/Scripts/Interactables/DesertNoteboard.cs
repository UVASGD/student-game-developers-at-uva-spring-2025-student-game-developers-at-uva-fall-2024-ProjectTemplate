using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class DesertNoteboard : Interactable
{
    public Transform noteboardViewTransform;

    public override void Interact()
    {
        // Open up read note UI or just freeze camera towards note
        if (canInteract)
        {
            // Debug.Log("note");
            CameraZoomManager.Instance.player.GetComponent<PlayerMovement>().enabled = false;
            CameraZoomManager.Instance.player.GetComponent<PlayerCameraMovement>().enabled = false;
            Camera.main.transform.DOMove(noteboardViewTransform.position, CameraZoomManager.Instance.cameraMoveZoomTime);
            Camera.main.transform.DORotate(noteboardViewTransform.rotation.eulerAngles, CameraZoomManager.Instance.cameraMoveZoomTime);
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
        if (!canInteract && Input.GetKeyDown(KeyCode.E))
        {
            Camera.main.transform.DOMove(CameraZoomManager.Instance.player.transform.position, CameraZoomManager.Instance.cameraMoveZoomTime);
            Camera.main.transform.DORotate(CameraZoomManager.Instance.player.transform.rotation.eulerAngles, CameraZoomManager.Instance.cameraMoveZoomTime).OnComplete(
                () => {
                    CameraZoomManager.Instance.player.GetComponent<PlayerMovement>().enabled = true;
                    CameraZoomManager.Instance.player.GetComponent<PlayerCameraMovement>().enabled = true;
                }
            );
            canInteract = true;
        }
    }
}