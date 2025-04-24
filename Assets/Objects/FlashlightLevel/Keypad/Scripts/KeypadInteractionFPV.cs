using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad { 
public class KeypadInteractionFPV : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private void Awake() => cam = Camera.main;
    private void Update()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                {
                    // Debug.Log("Hit object: " + hit.collider.gameObject.name);
                    keypadButton.PressButton();
                }
            }
        }
    }
}
}