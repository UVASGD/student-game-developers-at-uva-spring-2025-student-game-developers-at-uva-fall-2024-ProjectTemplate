using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // texture stolen from: https://www.mediafire.com/view/gchmkenjry6gsyh/FlashlightCookie.tif/file#
    [SerializeField] GameObject flashlightBeam;
    private bool flashlightActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }
}
