using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    bool isHolding = false;

    [SerializeField]
    float maxDistance = 2f;
    float distance;

    [SerializeField]
    TempParent tempParent;

    Rigidbody rb;

    public void Interact()
    {
        // When the I key is pressed down, try to pick up the object
        // When the I key is pressed again, the object will be dropped
        if (isHolding)
        {
            Drop();
        }
        else
        {
            AttemptPickUp();
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempParent = TempParent.Instance;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Update()
    {
        // If the object is being held, update its status
        if (isHolding)
        {
            Hold();
        }
    }

    private void AttemptPickUp()
    {
        if (tempParent != null)
        {
            // Keep track of the distance between the held object and the player
            // If the object is too far, then the item cannot continue to be held
            distance = Vector3.Distance(transform.position, tempParent.transform.position);
            Debug.Log($"Distance to TempParent: {distance}");

            if (distance <= maxDistance)
            {
                isHolding = true;
                rb.useGravity = false;
                rb.detectCollisions = true;
                transform.SetParent(tempParent.transform);
            }
            else
            {
                Debug.Log("Object is too far to pick up.");
            }
        }
        else
        {
            Debug.Log("Temp Parent item not found in scene");
        }
    }

    private void Hold()
    {
        distance = Vector3.Distance(transform.position, tempParent.transform.position);
        Debug.Log($"Holding - distance: {distance}");

        // Drop the object if it gets too far from the tempParent / player
        if (distance >= maxDistance)
        {
            Drop();
        }

        // Reset velocities to prevent unwanted physics interactions
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    // Drop the object and restore its physics properties
    private void Drop()
    {
        if (isHolding)
        {
            isHolding = false;
            transform.SetParent(null);
            rb.useGravity = true;
            rb.constraints = ~RigidbodyConstraints.FreezePositionY;

            // Reset only the z-axis rotation
            Vector3 currentEuler = transform.localEulerAngles;
            currentEuler.z = 0f;
            transform.localEulerAngles = currentEuler;
        }
    }
}
