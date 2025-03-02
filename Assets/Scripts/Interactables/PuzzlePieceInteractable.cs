using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceInteractable : Interactable
{
    public static int num_pieces = 0;
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
                // code block
                break;
            case 2:
                // code block
                break;
            case 3:
                // code block
                break;
            case 4:
                // code block
                break;
            case 5:
                // code block
                break;
            case 6:
                // code block
                break;
            default:
                // code block
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
}