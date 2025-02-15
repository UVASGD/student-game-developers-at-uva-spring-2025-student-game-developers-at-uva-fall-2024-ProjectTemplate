using UnityEngine;

[RequireComponent(typeof(Collider))] 
public class TeleportingIllusion : TeleportingIllusionBase
{
    [SerializeField] protected Vector3 direction;
    [SerializeField] private float angleTolerance;

    protected Collider coll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coll = GetComponent<Collider>();
    }


    public virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            Debug.Log(Vector3.Angle(Camera.main.transform.forward, direction));
            if (direction == Vector3.zero || Vector3.Angle(Camera.main.transform.forward, direction) < angleTolerance)
            {
                teleportObject(player);
            }
        }
    }
    public override void teleportObject(GameObject go)
    {
        Vector3 positionOffset = Quaternion.FromToRotation(destination.forward, direction) * (go.transform.position - transform.position);
        go.transform.position = destination.position + positionOffset;
        Debug.Log("offset" + positionOffset + "final pos" + (destination.position + positionOffset));
        Vector3 rotationOffset = Quaternion.FromToRotation(go.transform.forward, direction).eulerAngles;
        go.transform.eulerAngles = destination.eulerAngles + rotationOffset;
    }
}
