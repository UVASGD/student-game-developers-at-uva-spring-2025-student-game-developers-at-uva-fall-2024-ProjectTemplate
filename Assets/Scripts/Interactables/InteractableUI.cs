using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InteractableUI : MonoBehaviour {    
    public static InteractableUI Instance;
    [Tooltip("The GameObject that will be displayed when the player is in range of an interactable object")]
    public GameObject interactUI_GO;
    [Tooltip("The text that will be displayed on the interactable object")]
    public TextMeshProUGUI interactUI_Text;
    [Tooltip("The distance the player can start interacting with objects")]
    public float interactDistance = 5f;
    [Tooltip("The radius of the sphere cast used to detect interactable objects")]
    public float sphereCastRadius = 0.5f;
    [Tooltip("The time it takes for the interact UI to scale up and down")]
    public float scaleTime = 0.25f;

    void Awake(){
        if (Instance == null) {
            Instance = this;
            interactUI_GO.transform.localScale = Vector3.zero;
        } else {
            Destroy(gameObject);
        }
    }

    void Update(){
        HandleInteract();
    }

    void HandleInteract(){
        // Raycast from player position in direction of mouse to screen and see if it hits any objects with the layer "Interactable"
        if (Camera.main != null){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.5f, out hit, interactDistance, LayerMask.GetMask("Interactable"))){
                Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
                if (interactable != null && interactable.canInteract){
                    interactUI_GO.transform.DOScale(Vector3.one, scaleTime);
                    interactable.SetText();
                    if (Input.GetKeyDown(KeyCode.I)){
                        interactable.Interact();
                        interactUI_GO.transform.DOScale(Vector3.zero, scaleTime);
                    }
                } else {
                    interactUI_GO.transform.DOScale(Vector3.zero, scaleTime);
                }
            } else {
                interactUI_GO.transform.DOScale(Vector3.zero, scaleTime);
            }
        }
    }
}