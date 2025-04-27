using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flashlight : MonoBehaviour
{
    // texture stolen from: https://www.mediafire.com/view/gchmkenjry6gsyh/FlashlightCookie.tif/file#
    [SerializeField] public GameObject flashlightBeam;
    List<QuantumObjectBase> allObjects = new List<QuantumObjectBase>(); 
    private bool flashlightActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashlightActive = false;
        //flashlightBeam.gameObject.SetActive(flashlightActive);
        flashlightBeam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightActive = !flashlightActive;
            flashlightBeam.SetActive(flashlightActive);
            Debug.Log("flashlight triggered: " + flashlightActive);
        }
        //flashlightBeam.gameObject.SetActive(flashlightActive);

        // instantiating lists early because we need to use it in the else statement
        List<QuantumObjectBase> rayList = new List<QuantumObjectBase>();
        List<QuantumObjectBase> toRemove = new List<QuantumObjectBase>();
            
        // raycasting
        if (flashlightActive)
        {
            int flashlightLayer = LayerMask.GetMask("FlashlightCast");
            RaycastHit[] rayHits = Physics.SphereCastAll(
                transform.position,
                2f,
                transform.TransformDirection(Vector3.forward),
                8f,
                flashlightLayer
            );
            
            foreach (RaycastHit hit in rayHits)
            {
                QuantumObjectBase curObj = hit.collider.GetComponent<QuantumObjectBase>();
                rayList.Add(curObj);
                if (!(allObjects.Contains(curObj)))
                {
                    curObj.isLit = true;
                    allObjects.Add(curObj);
                }
            }

            // in allObjects NOT IN rayHits
            foreach (QuantumObjectBase obj in allObjects)
            {
                if (!(rayList.Contains(obj)))
                {
                    obj.isLit = false;
                    toRemove.Add(obj);
                }
            }
            foreach(QuantumObjectBase obj in toRemove)
            {
                allObjects.Remove(obj);
            }
        }
        else
        {
            foreach(QuantumObjectBase obj in allObjects)
            {
                obj.isLit = false;
                obj.onLookAway();
            }
            rayList.Clear();
            allObjects.Clear();
        }
    }
}
