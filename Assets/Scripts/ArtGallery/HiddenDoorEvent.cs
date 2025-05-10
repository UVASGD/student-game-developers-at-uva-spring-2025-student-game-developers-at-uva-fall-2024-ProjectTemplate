using UnityEngine;

public class HiddenDoorEvent : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject painting;

    public void OpenHiddenDoor()
    {
        if (painting != null && painting.activeInHierarchy)
        {
            // Deactivate the painting collider
            Collider paintingCollider = painting.GetComponent<Collider>();
            
            if (paintingCollider != null)
            {
                paintingCollider.enabled = false;
                Debug.Log("Painting collider has been deactivated.");
            }
        }

        if (wall != null && wall.activeInHierarchy)
        {
            // Deactivate the wall  / remove the wall back to the scene
            wall.SetActive(false);
            Debug.Log("Wall has been deactivated and removed from the scene.");
        }
    }
}
