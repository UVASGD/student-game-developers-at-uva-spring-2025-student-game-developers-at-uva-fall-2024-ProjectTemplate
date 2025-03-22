using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // texture stolen from: https://www.mediafire.com/view/gchmkenjry6gsyh/FlashlightCookie.tif/file#
    [SerializeField] GameObject flashlightBeam;
    private bool flashlightActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashlightActive = false;
        flashlightBeam.gameObject.SetActive(flashlightActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightActive = !flashlightActive;
            Debug.Log("flashlight triggered: " + flashlightActive);
        }
        flashlightBeam.gameObject.SetActive(flashlightActive);

        // raycasting
        if (flashlightActive)
        {
            bool ray = Physics.SphereCast(
                transform.position,
                1f,
                transform.TransformDirection(Vector3.forward),
                out RaycastHit hitInfo,
                8f
            );
            if (ray)
            {
                Debug.Log("hit something");
            }
            else
            {
                Debug.Log("nothing is hit");
            }
        }
    }
}
