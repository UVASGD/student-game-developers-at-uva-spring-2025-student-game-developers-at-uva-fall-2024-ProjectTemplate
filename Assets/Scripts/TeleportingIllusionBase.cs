using UnityEngine;

public class TeleportingIllusionBase : MonoBehaviour
{
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected Transform destination;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void teleportObject(GameObject go)
    {

        Vector3 positionOffset = Quaternion.FromToRotation(destination.forward, direction) * (go.transform.position - transform.position);
        go.transform.position = destination.position + positionOffset;
        Debug.Log("offset" + positionOffset + "final pos" + (destination.position + positionOffset));
        Vector3 rotationOffset = Quaternion.FromToRotation(go.transform.forward, direction).eulerAngles;
        go.transform.eulerAngles = destination.eulerAngles + rotationOffset;
    }
}
