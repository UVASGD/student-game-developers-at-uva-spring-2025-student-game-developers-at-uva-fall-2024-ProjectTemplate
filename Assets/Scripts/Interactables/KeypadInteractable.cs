using DG.Tweening;
using UnityEngine;

public class KeypadInteractable : Interactable
{
    public Transform keypadViewTransform;

    public override void Interact()
    {
        // Open up read note UI or just freeze camera towards note
        if (canInteract)
        {
            FlashlightRoomManager.Instance.player.GetComponent<PlayerMovement>().enabled = false;
            FlashlightRoomManager.Instance.player.GetComponent<PlayerCameraMovement>().enabled = false;
            Camera.main.transform.DOMove(keypadViewTransform.position, FlashlightRoomManager.Instance.cameraMoveZoomTime);
            Camera.main.transform.DORotate(keypadViewTransform.rotation.eulerAngles, FlashlightRoomManager.Instance.cameraMoveZoomTime);
            canInteract = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public override void SetText()
    {
        base.SetText();
        InteractableUI.Instance.interactUI_Text.text = "Open Keypad";

        // Move the text UI down to y = -80
        RectTransform textRect = InteractableUI.Instance.interactUI_Text.GetComponent<RectTransform>();
        Vector3 newPos = textRect.anchoredPosition;
        newPos.y = -80f;
        textRect.anchoredPosition = newPos;
    }

    public override void Update()
    {
        base.Update();
        if (!canInteract && Input.GetKeyDown(KeyCode.E))
        {
            Camera.main.transform.DOMove(FlashlightRoomManager.Instance.player.transform.position, FlashlightRoomManager.Instance.cameraMoveZoomTime);
            Camera.main.transform.DORotate(FlashlightRoomManager.Instance.player.transform.rotation.eulerAngles, FlashlightRoomManager.Instance.cameraMoveZoomTime).OnComplete(
                () => {
                    FlashlightRoomManager.Instance.player.GetComponent<PlayerMovement>().enabled = true;
                    FlashlightRoomManager.Instance.player.GetComponent<PlayerCameraMovement>().enabled = true;
                }
            );
            canInteract = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
