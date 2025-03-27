// using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject head;
    [SerializeField] private InputAction mouseX;
    [SerializeField] private InputAction mouseY;
    [SerializeField] private float mouseSense;
    public bool canPan = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseX.Enable();
        mouseY.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(canPan)
        {
            float inputValueX = mouseX.ReadValue<float>();
            float inputValueY = -mouseY.ReadValue<float>();
            if (inputValueX != 0)
            {
                gameObject.transform.rotation *= Quaternion.Euler(0, inputValueX * Time.deltaTime * mouseSense, 0);
            }
            if (inputValueY != 0)
            {
                head.transform.localRotation *= Quaternion.Euler(inputValueY * Time.deltaTime * mouseSense, 0, 0);
                head.transform.localRotation = Quaternion.Euler(head.transform.localEulerAngles.x, 0, 0);
            }
        }
    }
}
