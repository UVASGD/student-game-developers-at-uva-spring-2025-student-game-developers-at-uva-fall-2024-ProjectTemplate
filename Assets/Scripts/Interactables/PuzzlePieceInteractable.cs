using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceInteractable : Interactable
{
    public static int num_pieces = 0;

    // The painting GameObject whose material should get updated
    [SerializeField] private GameObject painting;

    // Array of materials to be assigned in order 
    [SerializeField] private Material[] paintingMaterials;

    [SerializeField] private GameObject hiddenDoorEventObject;


    public override void Interact()
    {
        // Debug.Log("Interacted with Puzzle Piece");


        // Increase the number of puzzle pieces that the player has
        num_pieces++;
        // Debug.Log($"Number of pieces: {num_pieces}");


        // Modify the Hidden Door Painting material
        switch (num_pieces)
        {
            case 1:
                // Add first painting piece to hidden door
                SetPaintingMaterial(0);

                break;
            case 2:
                // Add second painting piece to hidden door
                SetPaintingMaterial(1);

                break;
            case 3:
                // Add third painting piece to hidden door
                SetPaintingMaterial(2);

                break;
            case 4:
                // Add fourth painting piece to hidden door
                SetPaintingMaterial(3);

                break;
            case 5:
                // Add fifth painting piece to hidden door
                SetPaintingMaterial(4);

                break;
            case 6:
                // Add last painting piece to hidden door
                SetPaintingMaterial(5);

                // Open the Hidden Door
                HiddenDoorEvent hiddenDoorEvent = hiddenDoorEventObject.GetComponent<HiddenDoorEvent>();
                if (hiddenDoorEvent != null)
                {
                    hiddenDoorEvent.OpenHiddenDoor();
                }
                else
                {
                    Debug.LogWarning("HiddenDoorEvent component not found on the assigned GameObject.");
                }

                num_pieces = 0;

                break;
            default:
                break;
        }


        // Delete the piece from scene
        Destroy(gameObject);
        // Debug.Log("");
    }


    public override void SetText()
    {
        base.SetText();
        InteractableUI.Instance.interactUI_Text.text = "Interact";
    }


    // Helper method to update the painting's material
    private void SetPaintingMaterial(int index)
    {
        if (painting == null)
        {
            Debug.LogWarning("Painting GameObject is not assigned.");
            return;
        }

        MeshRenderer renderer = painting.GetComponent<MeshRenderer>();
        if (renderer == null)
        {
            Debug.LogWarning("No MeshRenderer component found on the painting GameObject.");
            return;
        }

        if (paintingMaterials == null || index >= paintingMaterials.Length)
        {
            Debug.LogWarning("Painting materials array is not set up correctly.");
            return;
        }

        renderer.material = paintingMaterials[index];
        // Debug.Log($"Painting material updated to index {index}.");
    }


    // Reset the static counter when the game is closed
    private void OnApplicationQuit()
    {
        num_pieces = 0;
    }

}