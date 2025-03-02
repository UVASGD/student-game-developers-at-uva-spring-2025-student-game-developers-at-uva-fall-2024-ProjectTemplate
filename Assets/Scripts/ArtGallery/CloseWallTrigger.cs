using UnityEngine;

public class CloseWallTrigger : MonoBehaviour
{
    [SerializeField] private GameObject wall;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player or a specific object
        if (other.CompareTag("Player"))
        {
            if (wall != null && !wall.activeInHierarchy)
            {
                // Activate the wall  / add the wall back to the scene
                wall.SetActive(true);
                //Debug.Log("Wall has been re-activated and added back to the scene.");
            }
        }
    }
}
