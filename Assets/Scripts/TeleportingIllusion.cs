using UnityEngine;

[RequireComponent(typeof(Collider))] 
public class TeleportingIllusion : TeleportingIllusionBase
{
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
}
