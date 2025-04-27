using UnityEngine;

public class HiddenDoorEvent : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject painting;
    [SerializeField] private GameObject desertWall;
    [SerializeField] private GameObject noteboard;
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject pin;

    public void Start()
    {
        desertWall.SetActive(false);
        noteboard.GetComponent<Renderer>().enabled = false;
        noteboard.GetComponent<MeshCollider>().enabled = false;
        note.SetActive(false);
        pin.SetActive(false);
    }

    public void OpenHiddenDoor()
    {
        if (painting != null && painting.activeInHierarchy)
        {
            // Deactivate the painting collider
            Collider paintingCollider = painting.GetComponent<Collider>();
            
            if (paintingCollider != null)
            {
                paintingCollider.enabled = false;
                // Debug.Log("Painting collider has been deactivated.");
            }
        }

        if (wall != null && wall.activeInHierarchy)
        {
            // Deactivate the wall  / remove the wall back to the scene
            wall.SetActive(false);
            // Debug.Log("Wall has been deactivated and removed from the scene.");
        }


        if (desertWall != null && !desertWall.activeInHierarchy)
        {
            // Activate the noteboard wall
            desertWall.SetActive(true);
            // Debug.Log("Noteboard wall has been activated.");
        }

        if (noteboard != null && noteboard.activeInHierarchy)
        {
            // Make noteboard visible and collideable
            noteboard.GetComponent<Renderer>().enabled = true;
            noteboard.GetComponent<MeshCollider>().enabled = true;
            // Debug.Log("Noteboard has been visualized.");
        }

        if (note != null && !note.activeInHierarchy)
        {
            // Activate the note
            note.SetActive(true);
            // Debug.Log("Note has been activated.");
        }

        if (pin != null && !pin.activeInHierarchy)
        {
            // Activate the pin
            pin.SetActive(true);
            // Debug.Log("Pin has been activated.");
        }
    }
}
