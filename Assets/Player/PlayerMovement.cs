using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private InputAction move;
    private Rigidbody rb;
    private bool canMove = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        move.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (move.ReadValue<Vector2>() != Vector2.zero)
            {
                Vector2 inputVec = move.ReadValue<Vector2>();
                rb.AddForce(transform.rotation * new Vector3(inputVec.x, 0, inputVec.y) * acceleration);
            }
            Vector2 horizontalVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z);
            rb.linearVelocity = new Vector3(Vector2.ClampMagnitude(horizontalVelocity, speed).x, rb.linearVelocity.y ,Vector2.ClampMagnitude(horizontalVelocity, speed).y);
            rb.linearVelocity = new Vector3(0.95f * rb.linearVelocity.x, rb.linearVelocity.y, 0.95f * rb.linearVelocity.z);
        }
    }

    public void lockMovement()
    {
        canMove = false;
    }

    public void unlockMovement()
    {
        canMove = true;
    }
}
